using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDog.Core
{
    class DefaultMessageBroker : IMessageBroker
    {
        public void Publish<T>(T message)
        {
            throw new NotImplementedException();
        }

        public Guid Subscribe<T>(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T>(Guid subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}
