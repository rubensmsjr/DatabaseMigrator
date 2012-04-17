using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Database;
using DatabaseMigrator.Config;

namespace DatabaseMigrator.Test.Database
{
    [TestClass, DeploymentItem("DatabaseSource.mdb"), DeploymentItem("DatabaseTarget.mdb")]
    public class DBMigrationTest
    {
        [TestMethod]
        public void TestInitializeConnection()
        {
            TableMigration tableMigration = new TableMigration(new ColumnMigrator());
            DBMigration dbMigration = new DBMigration(tableMigration);

            DBConfig dbConfigSource = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConfig dbConfigTarget = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseTarget.mdb", AppDomain.CurrentDomain.BaseDirectory));

            dbMigration.InitializeConnection(dbConfigSource, dbConfigTarget);

            Assert.IsTrue(dbMigration.TableMigration.DBConnectionSource.IsInitialized);
            Assert.IsTrue(dbMigration.TableMigration.DBConnectionTarget.IsInitialized);
        }
    }
}
