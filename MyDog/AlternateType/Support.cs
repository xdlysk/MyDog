using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common; 
using System.Reflection;

namespace MyDog.AlternateType
{
    public static class Support
    {
        public static DbProviderFactory TryGetProviderFactory(this DbConnection connection)
        {
            // If we can pull it out quickly and easily
            var profiledConnection = connection as DogDbConnection;
            if (profiledConnection != null)
            {
                return profiledConnection.InnerProviderFactory;
            }

#if (NET45)
            return DbProviderFactories.GetFactory(connection);
#else
            return connection.GetType().GetProperty("ProviderFactory", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(connection, null) as DbProviderFactory;
#endif
        }

        public static DbProviderFactory TryGetProfiledProviderFactory(this DbConnection connection)
        {
            var factory = connection.TryGetProviderFactory();
            if (factory != null)
            { 
                if (!(factory is DogDbProviderFactory))
                {
                    factory = factory.WrapProviderFactory(); 
                }
            }
            else
            {
                throw new NotSupportedException(string.Format("Glimpse requires that we can find the underlying DbProviderFactory from within your connection. Your current connection of type '{0}' does not support this functionality. If you control the implementation, changing this so we can support you shouldn't be that difficult.", connection.GetType().FullName));
            }

            return factory;
        }

        public static DbProviderFactory WrapProviderFactory(this DbProviderFactory factory)
        {
            if (!(factory is DogDbProviderFactory))
            { 
                var factoryType = typeof(DogDbProviderFactory<>).MakeGenericType(factory.GetType());
                return (DbProviderFactory)factoryType.GetField("Instance").GetValue(null);    
            }

            return factory;
        }

        public static DataTable FindDbProviderFactoryTable()
        {
            var providerFactories = typeof(DbProviderFactories);
            var providerField = providerFactories.GetField("_configTable", BindingFlags.NonPublic | BindingFlags.Static) ?? providerFactories.GetField("_providerTable", BindingFlags.NonPublic | BindingFlags.Static);
            var registrations = providerField.GetValue(null);
            return registrations is DataSet ? ((DataSet)registrations).Tables["DbProviderFactories"] : (DataTable)registrations;
        }

        public static object GetParameterValue(IDataParameter parameter)
        {
            if (parameter.Value == DBNull.Value)
            {
                return "NULL";
            }

            if (parameter.Value is byte[])
            {
                var blob = parameter.Value as byte[];
                return "BLOB" + (blob != null ? string.Format(" {0} bytes", blob.Length) : string.Empty);
            }

            return parameter.Value;
        }

        public static TimeSpan LogCommandSeed(this DogDbCommand command)
        {
            return TimeSpan.Zero;
        }

        public static void LogCommandStart(this DogDbCommand command, Guid commandId, TimeSpan timerTimeSpan)
        {
        }

        public static void LogCommandStart(this DogDbCommand command, Guid commandId, TimeSpan timerTimeSpan, bool isAsync)
        {
        }

        public static void LogCommandEnd(this DogDbCommand command, Guid commandId, TimeSpan timer, int? recordsAffected, string type)
        {
        }

        public static void LogCommandEnd(this DogDbCommand command, Guid commandId, TimeSpan timer, int? recordsAffected, string type, bool isAsync)
        {
        }

        public static void LogCommandError(this DogDbCommand command, Guid commandId, TimeSpan timer, Exception exception, string type)
        {
        }

        public static void LogCommandError(this DogDbCommand command, Guid commandId, TimeSpan timer, Exception exception, string type, bool isAsync)
        {
            
        }
    }
}
