# SignalR Message

## Introdução

O SignalR é uma bibilioteca para implementação assíncrona de comunicação em tempo real entre servidor e cliente.

## Servidor

### SignalR Hub

Um _hub_ do SignalR, representa um canal direto de alto nível para a comunicação entre clientes e o servidor, assim a classe `Hub` a qual o `MessageHub` deriva, é responsável por gerencia conexões, grupos e sistemas de mensagens.

```csharp
public class MessageHub : Hub
{
    public Task SendMessage(string user, string message) =>
        Clients.All?.SendAsync("ReceiveMessage", user, message);
}
```

Os métodos implementados no `MessageHub`, passam a ser acessíveis aos clientes conectados para enviar mensagens privadas, ou a todos os clientes.

### Configuração

No `ConfigureServices` da aplicação adicionar o suporte ao _SignalR_.

```csharp
services.AddSignalR();
```

Ao derivar da classe `Hub` o `MessageHub` passa a ser passível de ser cadastrada como um _hub_ da aplicação, através do `Configure` na `Startup`, junto ao mapeamento de _controllers_.

```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/messagehub");
});
```

Utilizada para receber as mensagens do SignalR o `On` possibilita que o cliente possua um canal aberto de comunicação com o servidor, no caso escutando mensagens enviadas ao método _ReceiveMessage_.

```csharp
hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
{
    messages.Add((user, message));
    StateHasChanged();
});
```

### Autenticação

`IUserIdProvider`

```csharp
public class UsernameBasedUserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection) =>
        connection.User?.FindFirst(ClaimTypes.Name)?.Value;
}
```

`services.AddSingleton<IUserIdProvider, UsernameBasedUserIdProvider>();`

Buscar Token a partir de parâmetro em _query string_, para requisições a rotas que possuam segmento iniciado por `/messagehub`.

```csharp
.AddJwtBearer(x =>
{
    // (...)
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
    
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/messagehub"))
                context.Token = accessToken;
    
            return Task.CompletedTask;
        }
    };
}
```

## Cliente

Utilizando métodos definidos no `MessageHub` atráves do `Send` disponibilizado pelo SignalR, através do cliente.

```csharp
await hubConnection.SendAsync("SendMessage", messageTo, messageInput);
// E/OU
await hubConnection.SendAsync("SendPrivateMessage", messageTo, messageInput);
```

## Referências

- [Introdução ao ASP.NET Core SignalR](https://docs.microsoft.com/pt-br/aspnet/core/tutorials/signalr?view=aspnetcore-5.0&tabs=visual-studio)