using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Reflection;
using Mvp.Xml.XInclude;
using DatabaseMigrator.XMLConfigurator;
using DatabaseMigrator.Logger;
using DatabaseMigrator.Resources;

namespace DatabaseMigrator.Config
{
    public class CreateConfig:ICreateConfig 
    {
        public DBConfig getBDConfig(string type)
        {
            return getBDConfig(type, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ConfigDBMigrator.xml"));
        }

        public DBConfig getBDConfig(string type, string fileDBConfig)
        {
            Configuration configuration = GetSettings<Configuration>(fileDBConfig);

            XMLConfigurator.Database database = configuration.ListDatabase.Find(delegate(XMLConfigurator.Database db) { return db.Type.Equals(type, StringComparison.InvariantCultureIgnoreCase); });
            if (database == null) { throw new ArgumentException(ResourceManager.GetMessage("TypeSourceOrTarget")); }

            return BuildSettings<DBConfig>(database.ListParameters);
        }

        private T GetSettings<T>(string fileName)
        {
            try
            {
                XmlTextReader xmlReader = new XmlTextReader(fileName);
                XIncludingReader xiReader = new XIncludingReader(xmlReader);
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                if (!serializer.CanDeserialize(xiReader))
                {
                    throw new InvalidOperationException(ResourceManager.GetMessage("XMLNotRead"));
                }

                T config = (T)serializer.Deserialize(xiReader);
                xiReader.Close();

                return config;
            }
            catch
            {
                throw new FileNotFoundException(ResourceManager.GetMessage("XMLNotFound"));
            }
        }

        private W BuildSettings<W>(List<Parameter> listParameter)
        {
            try
            {
                W sets = (W)Activator.CreateInstance(typeof(W));
                PropertyInfo propertyInfo;
                object value;

                foreach (Parameter parameter in listParameter)
                {
                    propertyInfo = sets.GetType().GetProperty(parameter.Name);
                    value = Convert.ChangeType(parameter.Value, propertyInfo.PropertyType);
                    propertyInfo.SetValue(sets, value, null);
                }
                return sets;
            }
            catch
            {
                throw new MissingFieldException(ResourceManager.GetMessage("ParamNotFound"));
            }
        }
    }
}
