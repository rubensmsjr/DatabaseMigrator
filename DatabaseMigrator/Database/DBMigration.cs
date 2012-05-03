using DatabaseMigrator.Config;

namespace DatabaseMigrator.Database
{
    public class DBMigration:IDBMigration
    {
        public ITableMigration TableMigration { get; set; }

        public DBMigration(ITableMigration tableMigration)
        {
            this.TableMigration = tableMigration;
        }

       public void InitializeConnection(DBConfig dbConfigSource, DBConfig dbConfigTarget)
       {
           IDBConnection DBConnectionSource = new DBConnection();
           DBConnectionSource.Initialize(dbConfigSource);

           IDBConnection DBConnectionTarget = new DBConnection();
           DBConnectionTarget.Initialize(dbConfigTarget);

           this.TableMigration.DBConnectionSource = DBConnectionSource;
           this.TableMigration.DBConnectionTarget = DBConnectionTarget;
       }
    }
}
