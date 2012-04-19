using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Config;
using DatabaseMigrator.Database;
using System.IO;

namespace DatabaseMigrator.Test.Database
{
    [TestClass, DeploymentItem("DatabaseSource.mdb"), DeploymentItem("DatabaseTarget.mdb")]
    public class DatabaseTest
    {
        [TestMethod]
        public void TestInitializeDBSource()
        {
            DBConfig dbConfig = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseSource.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBSource.Initialize(dbConfig);
            
            Assert.IsNotNull(DBSource.ProviderFactory);
            Assert.IsNotNull(DBSource.Connection);
            Assert.AreEqual(DBSource.Connection.State, ConnectionState.Open);
        }

        [TestMethod]
        public void TestInitializeDBTarget()
        {
            DBConfig dbConfig = new DBConfig("System.Data.OleDb", String.Format(@"Provider=Microsoft.JET.OLEDB.4.0;data source={0}\DatabaseTarget.mdb", AppDomain.CurrentDomain.BaseDirectory));
            DBTarget.Initialize(dbConfig);

            Assert.IsNotNull(DBTarget.ProviderFactory);
            Assert.IsNotNull(DBTarget.Connection);
            Assert.AreEqual(DBTarget.Connection.State, ConnectionState.Open);
        }
    }
}
