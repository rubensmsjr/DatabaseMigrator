using System.Collections.Generic;
using System.Xml.Serialization;

namespace DatabaseMigrator.XMLConfigurator
{
    public class Database
    {
        [XmlAttribute("type")]
        public string Type;

        [XmlArray("parameters")]
        [XmlArrayItem("parameter", typeof(Parameter))]
        public List<Parameter> ListParameters;
    }
}
