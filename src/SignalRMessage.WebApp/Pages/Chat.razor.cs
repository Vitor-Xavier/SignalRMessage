using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRMessage.WebApp.Crosscutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRMessage.WebApp.Pages
{
    public partial class Chat : ComponentBase
    {
        private HubConnection hubConnection;

        private ICollection<Message> messages = new HashSet<Message>();

        private string message;

        private readonly string _url = "https://localhost:5001";

        // Demonstrates how a parent component can supply parameters
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Type { get; set; }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            Console.WriteLine(firstRender);
            Console.WriteLine(Name);
            Console.WriteLine(string.Join(",", messages.Select(m => m.Content)));
            return base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder().WithUrl($"{_url}/messagehub", options =>
            {
                options.AccessTokenProvider = async () => StateContainer.Token;
            }).Build();

            hubConnection.On<string, string, string>("ReceiveMessage", (userFrom, userTo, message) =>
            {
                messages.Add(new Message { Content = message, From = userFrom, To = userTo, ReceivedDateTime = DateTime.Now });
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        async Task SendPrivate()
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            await hubConnection.SendAsync("SendPrivateMessage", Name, message);
            message = string.Empty;
        }
    }
}
