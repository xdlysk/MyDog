using System;
using System.Data;

namespace MyDog.Message
{
    public class TransactionBeganMessage : AdoTransactionMessage
    {
        public TransactionBeganMessage(Guid connectionId, Guid transactionId, IsolationLevel isolationLevel) 
            : base(connectionId, transactionId)
        {
            TransactionId = transactionId;
            IsolationLevel = isolationLevel;
        }

        public IsolationLevel IsolationLevel { get; protected set; }
    }
}