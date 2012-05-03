using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Config;
using DatabaseMigrator.Database;
using System.IO;

namespace DatabaseMigrator.Test.Database
{
    [TestClass, DeploymentItem("DatabaseSource.mdb")]
    public class DBConnectionTest
    {
        private IDBConnection dbConnection;

        [TestInitialize]
        public void InitializeCreateConfigTest()
        {
            dbConnection = new DBConnection();
        }

        [TestMethod]
        public void TestInitializeAndFinalize()
        {
            DBConfig dbConfig = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));

            dbConnection.Initialize(dbConfig);

            Assert.IsNotNull(dbConnection.ProviderFactory);
            Assert.IsNotNull(dbConnection.Connection);
            Assert.IsTrue(dbConnection.IsInitialized);
            Assert.AreEqual(dbConnection.Connection.State, ConnectionState.Open);

            dbConnection.Finalize();

            Assert.IsNull(dbConnection.ProviderFactory);
            Assert.IsNull(dbConnection.Connection);
            Assert.IsFalse(dbConnection.IsInitialized);
        }
    }
}
