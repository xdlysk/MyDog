using System;

namespace MyDog.Message
{
    public abstract class AdoTransactionMessage : AdoMessage
    {
        protected AdoTransactionMessage(Guid connectionId, Guid transactionId) 
            : base(connectionId)
        {
            TransactionId = transactionId;
        }

        public Guid TransactionId { get; protected set; }
    }
}