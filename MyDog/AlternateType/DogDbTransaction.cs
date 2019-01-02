using MyDog.Core;
using MyDog.Message;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDog.AlternateType
{
    public class DogDbTransaction:DbTransaction
    {
        private readonly TimeSpan timerTimeSpan;
        private IMessageBroker messageBroker;

        public DogDbTransaction(DbTransaction transaction, DogDbConnection connection)
        {
            InnerTransaction = transaction;
            InnerConnection = connection;
            TransactionId = Guid.NewGuid();

            if (MessageBroker != null)
            {

                MessageBroker.Publish(
                    new TransactionBeganMessage(connection.ConnectionId, TransactionId, transaction.IsolationLevel));
            }
        }

        public DogDbTransaction(DbTransaction transaction, DogDbConnection connection, IMessageBroker messageBroker)
            : this(transaction, connection)
        {
            MessageBroker = messageBroker;
        }

        public DogDbConnection InnerConnection { get; set; }

        public DbTransaction InnerTransaction { get; set; }

        public Guid TransactionId { get; set; }

        public override IsolationLevel IsolationLevel
        {
            get { return InnerTransaction.IsolationLevel; }
        }

        protected override DbConnection DbConnection
        {
            get { return InnerConnection; }
        }

        private IMessageBroker MessageBroker
        {
            get { return messageBroker ?? (messageBroker = DogConfiguration.GetConfiguredMessageBroker()); }
            set { messageBroker = value; }
        }

        public override void Commit()
        {
            InnerTransaction.Commit();

            if (MessageBroker != null)
            {
                MessageBroker.Publish(
                    new TransactionCommitMessage(InnerConnection.ConnectionId, TransactionId));
            }
        }

        public override void Rollback()
        {
            InnerTransaction.Rollback();

            if (MessageBroker != null)
            {
                MessageBroker.Publish(
                    new TransactionRollbackMessage(InnerConnection.ConnectionId, TransactionId));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                InnerTransaction.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
