namespace SignalRMessage.Crosscutting
{
    public class Message
    {
        public string Event { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Content { get; set; }
    }
}
