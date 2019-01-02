using System;

namespace MyDog.Message
{
    public class TransactionCommitMessage : AdoTransactionMessage
    {
        public TransactionCommitMessage(Guid connectionId, Guid transactionId) : base(connectionId, transactionId)
        {
        }
    }
}