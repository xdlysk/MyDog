using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyDog.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var test = System.Configuration.ConfigurationManager.ConnectionStrings["test"];
            DbProviderFactoriesExecutionTask dbProvider = new DbProviderFactoriesExecutionTask();
            dbProvider.Execute();

            var provider = DbProviderFactories.GetFactory(test.ProviderName);

            using (var conn = provider.CreateConnection())
            using (var cmd = provider.CreateCommand())
            {
                conn.ConnectionString = test.ConnectionString;
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "select * from t1";
                var dr = cmd.ExecuteReader();

            }
        }
    }
}
