using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseMigrator.Resources;
using System.Globalization;
using System.Threading;

namespace DatabaseMigrator.Test.Resources
{
    [TestClass]
    public class ResourceManagerTest
    {
        [TestMethod]
        public void TestGetMessage()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", true);
            Assert.AreEqual("Was not possible to delete table {0}.", ResourceManager.GetMessage("LogDeleteTable"));
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", true);
            Assert.AreEqual("Não foi possivel excluir tabela {0}.", ResourceManager.GetMessage("LogDeleteTable"));
        }
    }
}
