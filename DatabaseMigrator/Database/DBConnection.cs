using System.Data;
using System.Data.Common;
using DatabaseMigrator.Config;
using DatabaseMigrator.Resources;
using System;

namespace DatabaseMigrator.Database
{
    public class DBConnection:IDBConnection
    {
        public DbConnection Connection { get; set; }
        public DbProviderFactory ProviderFactory { get; set; }
        public bool IsInitialized { get; set; }

        public DBConnection()
        {
            IsInitialized = false;
        }

        public void Initialize(DBConfig dbConfig)
        {
            ProviderFactory = DbProviderFactories.GetFactory(dbConfig.Client);

            try
            {
                Connection = ProviderFactory.CreateConnection();
                Connection.ConnectionString = dbConfig.ConnectionString;
                Connection.Open();
            }
            catch (Exception ex)
            {
                throw new DataException(string.Format(ResourceManager.GetMessage("FailedOpenDatabase"),dbConfig.ConnectionString, ex.Message));
            }

            IsInitialized = true;
        }

        public void Finalize()
        {
            Connection.Close();
            Connection = null;
            ProviderFactory = null;
            IsInitialized = false;
        }
    }
}
