using System;

namespace MyDog.Message
{
    public class TransactionRollbackMessage : AdoTransactionMessage
    {
        public TransactionRollbackMessage(Guid connectionId, Guid transactionId) : base(connectionId, transactionId)
        {
        }
    }
}