using System;
using System.Xml.Serialization;

namespace DatabaseMigrator.XMLConfigurator
{
    [Serializable()]
    [XmlRoot("parameter")]
    public class Parameter
    {
        [XmlAttribute("name")]
        public String Name;

        [XmlAttribute("value")]
        public String Value;
    }
}
