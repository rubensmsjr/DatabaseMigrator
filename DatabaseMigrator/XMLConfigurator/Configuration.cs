using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DatabaseMigrator.XMLConfigurator
{
    [Serializable()]
    [XmlRoot("configuration")]
    public class Configuration
    {
        [XmlArray("databases")]
        [XmlArrayItem("database", typeof(Database))]
        public List<Database> ListDatabase;
    }
}
