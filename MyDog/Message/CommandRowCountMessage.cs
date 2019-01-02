using System;

namespace MyDog.Message
{
    public class CommandRowCountMessage : AdoCommandMessage
    {
        public CommandRowCountMessage(Guid connectionId, Guid commandId, long rowCount) : base(connectionId, commandId)
        {
            RowCount = rowCount;
        }

        public long RowCount { get; protected set; }
    }
}