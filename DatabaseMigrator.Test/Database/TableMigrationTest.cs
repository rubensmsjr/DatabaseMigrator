using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Database;
using DatabaseMigrator.Config;

namespace DatabaseMigrator.Test.Database
{
    [TestClass, DeploymentItem("DatabaseSource.mdb"), DeploymentItem("DatabaseTarget.mdb")]
    public class TableMigrationTest
    {
        [TestMethod]
        public void TestExecute()
        {
            DBConfig dbConfigSource = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConnection dbConnectionSource = new DBConnection();
            dbConnectionSource.Initialize(dbConfigSource);

            DBConfig dbConfigTarget = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseTarget.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConnection dbConnectionTarget = new DBConnection();
            dbConnectionTarget.Initialize(dbConfigTarget);

            TableMigration tableMigration = new TableMigration(new ColumnMigrator());
            tableMigration.DBConnectionSource = dbConnectionSource;
            tableMigration.DBConnectionTarget = dbConnectionTarget;

            tableMigration.Execute();

        }
    }
}
