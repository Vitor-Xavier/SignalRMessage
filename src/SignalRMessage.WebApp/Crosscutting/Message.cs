using System;

namespace SignalRMessage.WebApp.Crosscutting
{
    public class Message
    {
        public string Content { get; set; }

        public string To { get; set; }

        public DateTime ReceivedDateTime { get; set; }
    }
}
