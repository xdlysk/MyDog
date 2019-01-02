using System;

namespace MyDog.Message
{
    public class ConnectionStartedMessage : AdoMessage
    {
        public ConnectionStartedMessage(Guid connectionId) : base(connectionId)
        {
        }
    }
}