using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SignalRMessage.WebApp.Pages
{
    public partial class FetchData : ComponentBase
    {
        private readonly string _url = "https://localhost:5001";

        private WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            var response = await Http.GetAsync($"{_url}/api/WeatherForecast");
            response.EnsureSuccessStatusCode();
            forecasts = JsonConvert.DeserializeObject<WeatherForecast[]>(await response.Content.ReadAsStringAsync());
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
