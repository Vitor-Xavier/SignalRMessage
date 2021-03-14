using System;

namespace SignalRMessage.WebApp.Container
{
    public class StateContainer
    {
        public string Token { get; set; } = "";

        public event Action OnChange;

        public void SetProperty(string value)
        {
            Token = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
