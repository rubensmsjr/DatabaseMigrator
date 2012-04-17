using DatabaseMigrator.Config;

namespace DatabaseMigrator.Database
{
    public interface IDBMigration
    {
        ITableMigration TableMigration { get; set; }

        void InitializeConnection(DBConfig dbConfigSource, DBConfig dbConfigTarget);
    }
}
