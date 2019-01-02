using System;
using System.Diagnostics;
using System.Reflection;


namespace MyDog.Message
{
    public abstract class AdoMessage 
    {
        protected AdoMessage(Guid connectionId)
        {
            Id = Guid.NewGuid();
            ConnectionId = connectionId;
            StartTime = DateTime.Now;            
        }

        public Guid Id { get; private set; }

        public Guid ConnectionId { get; set; }

        public TimeSpan Offset { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime StartTime { get; set; }
    }
}