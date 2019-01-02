using MyDog.AlternateType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq; 

namespace MyDog
{
    public class DbProviderFactoriesExecutionTask 
    {
        public readonly static Dictionary<string, string> Factories = new Dictionary<string, string>(); 

        public DbProviderFactoriesExecutionTask()
        {
        }


        public void Execute()
        { 
            Console.WriteLine("AdoInspector: Starting to replace DbProviderFactory");

            // This forces the creation 
            try
            {
                DbProviderFactories.GetFactory("Anything"); 
            }
            catch (ArgumentException)
            { 
            }

            // Find the registered providers
            var table = Support.FindDbProviderFactoryTable();

            // Run through and replace providers
            foreach (var row in table.Rows.Cast<DataRow>().ToList())
            {
                DbProviderFactory factory;
                try
                {
                    factory = DbProviderFactories.GetFactory(row);

                    Console.WriteLine("AdoInspector: Successfully retrieved factory - {0}", row["Name"]);
                }
                catch (Exception)
                {
                    Console.WriteLine("AdoInspector: Failed to retrieve factory - {0}", row["Name"]);
                    continue;
                }

                // Check that we haven't already wrapped things up 
                if (factory is DogDbProviderFactory)
                {
                    Console.WriteLine("AdoInspector: Factory is already wrapped - {0}", row["Name"]);
                    continue;
                }

                var proxyType = typeof(DogDbProviderFactory<>).MakeGenericType(factory.GetType());

                Factories.Add(row["InvariantName"].ToString(), row["AssemblyQualifiedName"].ToString());

                var newRow = table.NewRow();
                newRow["Name"] = row["Name"];
                newRow["Description"] = row["Description"];
                newRow["InvariantName"] = row["InvariantName"];
                newRow["AssemblyQualifiedName"] = proxyType.AssemblyQualifiedName;

                table.Rows.Remove(row);
                table.Rows.Add(newRow);

                Console.WriteLine("AdoInspector: Successfully replaced - {0}", newRow["Name"]);
            }

            Console.WriteLine("AdoInspector: Finished replacing DbProviderFactory");
        }
    }
}