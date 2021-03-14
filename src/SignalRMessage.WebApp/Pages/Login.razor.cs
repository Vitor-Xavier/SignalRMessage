using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using SignalRMessage.WebApp.Crosscutting;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignalRMessage.WebApp.Pages
{
    public partial class Login : ComponentBase
    {
        private string username;

        private string password;

        private bool loggingIn = false;

        private readonly string _url = "https://localhost:5001";

        async Task Connect()
        {
            loggingIn = true;
            var user = new User { Username = username, Password = password };
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(user)).ConfigureAwait(false);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await Http.PostAsync($"{_url}/api/User/Authenticate", content);
            response.EnsureSuccessStatusCode();

            var logged = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());

            StateContainer.User = logged;
            StateContainer.SetProperty(logged.Token);

            NavigationManager.NavigateTo("");
            loggingIn = false;
        }
    }
}
