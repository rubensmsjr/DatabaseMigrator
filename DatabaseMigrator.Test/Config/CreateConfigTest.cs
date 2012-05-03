using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Config;
using System.Globalization;
using System.Threading;

namespace DatabaseMigrator.Test.Config
{
    [TestClass, DeploymentItem("ConfigDBMigrator.xml")]
    public class CreateConfigTest
    {
        private DBConfig dbConfig;
        private CreateConfig createConfig;

        [TestInitialize]
        public void InitializeCreateConfigTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", true);
            dbConfig = new DBConfig("System.Data.OleDb", "Provider=Microsoft.JET.OLEDB.4.0;data source=DatabaseMigrator.mdb");            
            createConfig = new CreateConfig();
        }

        [TestMethod]
        public void TestGetBDConfigTypeSource()
        {
            Assert.AreEqual(dbConfig, createConfig.getBDConfig("source"));
            Assert.AreEqual(dbConfig, createConfig.getBDConfig("Source"));
            Assert.AreEqual(dbConfig, createConfig.getBDConfig("SOURCE"));
        }

        [TestMethod]
        public void TestGetBDConfigTypeTarget()
        {
            Assert.AreEqual(dbConfig, createConfig.getBDConfig("target"));
            Assert.AreEqual(dbConfig, createConfig.getBDConfig("Target"));
            Assert.AreEqual(dbConfig, createConfig.getBDConfig("TARGET"));
        }

        [TestMethod]
        public void TestGetBDConfigTypeInvalid()
        {
            try
            {
                createConfig.getBDConfig("null");
            }
            catch (ArgumentException AE)
            {
                Assert.AreEqual("The type of database should be source or target.", AE.Message);
            }
        }

        [TestMethod]
        public void TestGetBDConfigXMLNotFound()
        {
            try
            {
                createConfig.getBDConfig("null","null");
            }
            catch (FileNotFoundException FNFE)
            {
                Assert.AreEqual("XML file not found.", FNFE.Message);
            }
        }
    }
}
