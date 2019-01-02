using System;
using System.Transactions;

namespace MyDog.Message
{
    public class DtcTransactionCompletedMessage : AdoMessage
    {
        public DtcTransactionCompletedMessage(Guid connectionId, TransactionStatus transactionStatus) : base(connectionId)
        {
            Status = transactionStatus;
        }

        public TransactionStatus Status { get; protected set; }
    }
}