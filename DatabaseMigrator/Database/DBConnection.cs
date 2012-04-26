using System.Data;
using System.Data.Common;
using DatabaseMigrator.Config;
using DatabaseMigrator.Resources;

namespace DatabaseMigrator.Database
{
    public class DBConnection:IDBConnetion
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
            catch
            {
                throw new DataException(ResourceManager.GetMessage("FailedOpenDatabase"));
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
