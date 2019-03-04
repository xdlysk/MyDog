using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace MyDog.Test
{
    class InterceptorSqlClient : SqlServerDatabaseProvider
    {
        public override DbProviderFactory GetFactory()
        {
            return GetFactory("MyDog.AlternateType.DogDbProviderFactory`1[[System.Data.SqlClient.SqlClientFactory, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], MyDog");
        }
    }
}
