using System;
using DatabaseMigrator.Config;
using DatabaseMigrator.Database;

namespace DatabaseMigrator
{
    public class Migrator:IMigrator
    {
        private ICreateConfig createConfig;
        public Migrator(ICreateConfig createConfig)
        {
            this.createConfig = createConfig;
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
            DBSource.Initialize(dbConfigSource);
            DBTarget.Initialize(dbConfigTarget);


        }
    }
}
