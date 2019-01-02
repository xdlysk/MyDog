using System;
using System.Transactions;

namespace MyDog.Message
{
    public class DtcTransactionEnlistedMessage : AdoMessage
    {
        public DtcTransactionEnlistedMessage(Guid connectionId, IsolationLevel level) : base(connectionId)
        {
            IsolationLevel = level;
        }

        public IsolationLevel IsolationLevel { get; protected set; }
    }
}