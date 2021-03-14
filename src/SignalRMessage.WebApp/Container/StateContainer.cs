using SignalRMessage.WebApp.Crosscutting;
using System;

namespace SignalRMessage.WebApp.Container
{
    public class StateContainer
    {
        public string Token { get; private set; } = "";

        public User User { get; set; }

        public event Action OnChange;

        public void SetProperty(string value)
        {
            Token = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
