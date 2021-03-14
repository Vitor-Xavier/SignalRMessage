using Microsoft.AspNetCore.Components;

namespace SignalRMessage.WebApp.Pages
{
    public partial class Counter : ComponentBase
    {
        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
        }
    }
}
