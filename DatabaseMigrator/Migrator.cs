using System;
using DatabaseMigrator.Config;
using DatabaseMigrator.Database;

namespace DatabaseMigrator
{
    public class Migrator:IMigrator
    {
        private ICreateConfig createConfig;
        private IDBMigration databaseMigration;

        public Migrator(ICreateConfig createConfig, IDBMigration databaseMigration)
        {
            this.createConfig = createConfig;
            this.databaseMigration = databaseMigration;
        }

        public void Execute()
        {
            Execute(this.createConfig.getBDConfig("source"), this.createConfig.getBDConfig("target"));
        }

        public void Execute(string fileDBConfig)
        {
            Execute(this.createConfig.getBDConfig("source", fileDBConfig), this.createConfig.getBDConfig("target", fileDBConfig));
        }

        public void Execute(DBConfig dbConfigSource, DBConfig dbConfigTarget)
        {
            databaseMigration.InitializeConnection(dbConfigSource, dbConfigTarget);
            databaseMigration.TableMigration.Execute();
        }
    }
}
