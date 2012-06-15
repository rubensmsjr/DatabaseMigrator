using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Config;
using DatabaseMigrator.Database;

namespace DatabaseMigrator.Test.Database
{
    [TestClass, DeploymentItem("DatabaseSource.mdb")]
    public class ColumnMigratorTest
    {
        [TestMethod]
        public void TestGetSQLCreateColumnsInTable()
        {
            DBConfig dbConfigSource = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));
            IDBConnection dbConnectionSource = new DBConnection();
            dbConnectionSource.Initialize(dbConfigSource);

            ColumnMigrator columnMigrator = new ColumnMigrator(new ConvertName());
            Assert.AreEqual(columnMigrator.GetSQLCreateColumnsInTable(dbConnectionSource.Connection, "TABLE1_TEST"), "(COLUMN1_VARCHAR  VARCHAR(100) ,COLUMN2_VARCHAR  VARCHAR(255) NOT NULL,COLUMN3_DATE DATE ,COLUMN4_BIT  VARCHAR(1) ,COLUMN5_INTEGER INTEGER ,COLUMN6_DECIMAL NUMBER )");
        }

        [TestMethod]
        public void TestGetSQLSelectColumnsInView()
        {

        }
    }
}
