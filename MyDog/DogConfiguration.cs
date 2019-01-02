using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDog.Core;

namespace MyDog
{
    class DogConfiguration
    {
        internal static IMessageBroker GetConfiguredMessageBroker()
        {
            return new DefaultMessageBroker();
        }
    }
}
