using System.Data;
using System.Data.Common;
using DatabaseMigrator.Config;

namespace DatabaseMigrator.Database
{
    public class DBTarget
    {
        public static IDbConnection Connection { get; set; }
        public static DbProviderFactory ProviderFactory { get; set; }

        public static void Initialize(DBConfig dbConfig)
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
                throw new DataException("Failed to open database.");
            }
        }

        public static void Finalize(DBConfig dbConfig)
        {
            Connection.Close();
            Connection = null;
            ProviderFactory = null;
        }
    }
}
