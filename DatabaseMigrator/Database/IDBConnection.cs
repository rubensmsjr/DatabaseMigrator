using System.Data;
using System.Data.Common;
using DatabaseMigrator.Config;

namespace DatabaseMigrator.Database
{
    public interface IDBConnection
    {
        DbConnection Connection { get; set; }
        DbProviderFactory ProviderFactory { get; set; }
        bool IsInitialized { get; set; }

        void Initialize(DBConfig dbConfig);
        void Finalize();
    }
}
