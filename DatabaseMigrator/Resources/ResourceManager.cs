using System.Threading;
using System.Reflection;
using System.Globalization;
using System;

namespace DatabaseMigrator.Resources
{
    public class ResourceManager
    {
        public static string GetMessage(string MessageName)
        {
            System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("DatabaseMigrator.Resources.Resource", typeof(ResourceManager).Assembly);
            return resourceManager.GetString(MessageName, System.Threading.Thread.CurrentThread.CurrentCulture);
        }
    }
}
