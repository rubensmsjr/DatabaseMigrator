using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Config;
using DatabaseMigrator.Database;
using DatabaseMigrator.Logger;

namespace DatabaseMigrator.Test.Database
{
    [TestClass, DeploymentItem("DatabaseSource.mdb"), DeploymentItem("DatabaseTarget.mdb")]
    public class ViewMigrationTest
    {
        [TestMethod]
        public void TestExecuteViewMigration()
        {
            DBConfig dbConfigSource = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConnection dbConnectionSource = new DBConnection();
            dbConnectionSource.Initialize(dbConfigSource);

            DBConfig dbConfigTarget = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseTarget.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConnection dbConnectionTarget = new DBConnection();
            dbConnectionTarget.Initialize(dbConfigTarget);

            ViewMigration viewMigration = new ViewMigration(new ConvertName(), new ColumnMigrator(new ConvertName()), new Log4NetLogger());
            viewMigration.DBConnectionSource = dbConnectionSource;
            viewMigration.DBConnectionTarget = dbConnectionTarget;

            viewMigration.Execute();
        }
    }
}
