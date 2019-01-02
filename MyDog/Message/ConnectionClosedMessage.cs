using System;


namespace MyDog.Message
{
    public class ConnectionClosedMessage : AdoMessage 
    {
        public ConnectionClosedMessage(Guid connectionId) 
            : base(connectionId)
        {
        }

        public string EventName { get; set; }


        public string EventSubText { get; set; }
    }
}