using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRMessage.WebApp.Crosscutting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRMessage.WebApp.Pages
{
    public partial class Index : ComponentBase
    {
        private HubConnection hubConnection;

        private readonly ICollection<Message> messages = new HashSet<Message>();

        private string messageInput;

        private string messageTo;

        private readonly string _url = "https://localhost:5001";

        public bool IsConnected => hubConnection?.State == HubConnectionState.Connected;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"{_url}/messagehub", options =>
            {
                options.AccessTokenProvider = async () => StateContainer.Token;
            }).Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                messages.Add(new Message { Content = message, To = user, ReceivedDateTime = DateTime.Now });
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        async Task Send()
        {
            if (string.IsNullOrWhiteSpace(messageInput))
                return;

            await hubConnection.SendAsync("SendMessage", messageInput);
            messageInput = string.Empty;
        }

        async Task SendPrivate()
        {
            if (string.IsNullOrWhiteSpace(messageTo) || string.IsNullOrWhiteSpace(messageInput))
                return;

            await hubConnection.SendAsync("SendPrivateMessage", messageTo, messageInput);
            messageInput = string.Empty;
        }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection is not null) await hubConnection.DisposeAsync();
        }
    }
}
