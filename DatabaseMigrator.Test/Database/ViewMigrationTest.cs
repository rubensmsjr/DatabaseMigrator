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
        }

        [TestMethod]
        public void TestExecuteViewMigration()
        {
            TableMigration tableMigration = new TableMigration(convertName, columnMigrator, logger);
            tableMigration.DBConnectionSource = dbConnectionSource;
            tableMigration.DBConnectionTarget = dbConnectionTarget;

            tableMigration.Execute();

            ViewMigration viewMigration = new ViewMigration(convertName, columnMigrator, logger);
            viewMigration.DBConnectionSource = dbConnectionSource;
            viewMigration.DBConnectionTarget = dbConnectionTarget;

            viewMigration.Execute();
        }
    }
}
