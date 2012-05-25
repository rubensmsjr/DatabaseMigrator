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
        private IDBConnection dbConnectionSource;
        private IDBConnection dbConnectionTarget;
        private IConvertName convertName;
        private IColumnMigrator columnMigrator;
        private ILogger logger;

        [TestInitialize]
        public void InitializeCreateConfigTest()
        {
            DBConfig dbConfigSource = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));
            dbConnectionSource = new DBConnection();
            dbConnectionSource.Initialize(dbConfigSource);

            DBConfig dbConfigTarget = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseTarget.mdb", AppDomain.CurrentDomain.BaseDirectory));
            dbConnectionTarget = new DBConnection();
            dbConnectionTarget.Initialize(dbConfigTarget);

            convertName = new ConvertName();
            columnMigrator = new ColumnMigrator(convertName);
            logger = new Log4NetLogger();

            TableMigration tableMigration = new TableMigration(convertName, columnMigrator, logger);
            tableMigration.DBConnectionSource = dbConnectionSource;
            tableMigration.DBConnectionTarget = dbConnectionTarget;

            tableMigration.Execute();
        }

        [TestMethod]
        public void TestExecuteTableMigration()
        {
            DbCommand dbCommandSource = dbConnectionTarget.Connection.CreateCommand();
            dbCommandSource.CommandText = "SELECT * FROM TABLE1_TEST";
            dbCommandSource.CommandType = CommandType.Text;
            Assert.IsTrue(dbCommandSource.ExecuteReader().HasRows);
        }

        [TestMethod]
        public void TestExecuteTableMigrationConvert()
        {
            DbCommand dbCommandSource = dbConnectionTarget.Connection.CreateCommand();
            dbCommandSource.CommandText = "SELECT * FROM TABLE4_TEST_CONVERT_TABLE_NAME";
            dbCommandSource.CommandType = CommandType.Text;
            Assert.IsTrue(dbCommandSource.ExecuteReader().HasRows);
        }
    }
}
