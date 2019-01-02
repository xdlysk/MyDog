using System;


namespace MyDog.Message
{
    public class CommandErrorMessage : AdoCommandMessage
    {
        public CommandErrorMessage(Guid connectionId, Guid commandId, Exception exception)
            : this(connectionId, commandId, exception, false)
        {
        }

        public CommandErrorMessage(Guid connectionId, Guid commandId, Exception exception, bool isAsync)
            : base(connectionId, commandId)
        {
            Exception = exception;
            IsAsync = isAsync;
        }

        public Exception Exception { get; protected set; }

        public string EventName { get; set; }

        public string EventSubText { get; set; }

        public bool IsAsync { get; set; }
    }
}