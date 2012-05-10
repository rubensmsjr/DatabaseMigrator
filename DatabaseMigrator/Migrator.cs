using System;
using DatabaseMigrator.Config;
using DatabaseMigrator.Database;
using DatabaseMigrator.Logger;

namespace DatabaseMigrator
{
    public class Migrator:IMigrator
    {
        private ICreateConfig createConfig;
        private IDBMigration databaseMigration;
        private ILogger logger;

        public Migrator(ILogger logger, ICreateConfig createConfig, IDBMigration databaseMigration)
        {
            this.createConfig = createConfig;
            this.databaseMigration = databaseMigration;
            this.logger = logger;
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
            try
            {
                databaseMigration.InitializeConnection(dbConfigSource, dbConfigTarget);
                databaseMigration.TableMigration.Execute();
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
