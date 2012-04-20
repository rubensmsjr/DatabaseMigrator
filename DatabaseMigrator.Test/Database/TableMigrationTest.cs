using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Database;
using DatabaseMigrator.Config;
using DatabaseMigrator.Logger;
using System.Data.Common;
using System.Data;

namespace DatabaseMigrator.Test.Database
{
    [TestClass, DeploymentItem("DatabaseSource.mdb"), DeploymentItem("DatabaseTarget.mdb")]
    public class TableMigrationTest
    {
        [TestMethod]
        public void TestExecuteTableMigration()
        {
            DBConfig dbConfigSource = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConnection dbConnectionSource = new DBConnection();
            dbConnectionSource.Initialize(dbConfigSource);

            DBConfig dbConfigTarget = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseTarget.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConnection dbConnectionTarget = new DBConnection();
            dbConnectionTarget.Initialize(dbConfigTarget);

            TableMigration tableMigration = new TableMigration(new ConvertName(), new ColumnMigrator(new ConvertName()), new Log4NetLogger());
            tableMigration.DBConnectionSource = dbConnectionSource;
            tableMigration.DBConnectionTarget = dbConnectionTarget;

            tableMigration.Execute();

            DbCommand dbCommandSource = dbConnectionTarget.Connection.CreateCommand();
            dbCommandSource.CommandText = "SELECT * FROM TABLE1_TEST";
            dbCommandSource.CommandType = CommandType.Text;
            Assert.IsTrue(dbCommandSource.ExecuteReader().HasRows);
        }

        [TestMethod]
        public void TestExecuteTableMigrationConvert()
        {
            DBConfig dbConfigSource = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConnection dbConnectionSource = new DBConnection();
            dbConnectionSource.Initialize(dbConfigSource);

            DBConfig dbConfigTarget = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseTarget.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBConnection dbConnectionTarget = new DBConnection();
            dbConnectionTarget.Initialize(dbConfigTarget);

            TableMigration tableMigration = new TableMigration(new ConvertName(), new ColumnMigrator(new ConvertName()), new Log4NetLogger());
            tableMigration.DBConnectionSource = dbConnectionSource;
            tableMigration.DBConnectionTarget = dbConnectionTarget;

            tableMigration.Execute();

            DbCommand dbCommandSource = dbConnectionTarget.Connection.CreateCommand();
            dbCommandSource.CommandText = "SELECT * FROM TABLE4_TEST_CONVERT_TABLE_NAME";
            dbCommandSource.CommandType = CommandType.Text;
            Assert.IsTrue(dbCommandSource.ExecuteReader().HasRows);
        }
    }
}
