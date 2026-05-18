using System.Xml.Serialization;

namespace RWAN10T.Api.Hypermdia
{
    public class HypermediaLink
    {
        [XmlAnyAttribute]
        public string Rel { get; set; } = String.Empty;
        [XmlAnyAttribute]
        public string Href { get; set; } = String.Empty;
        [XmlAnyAttribute]
        public string Type { get; set; } = "application/json";
        [XmlAnyAttribute]
        public string Action { get; set; } = String.Empty;

    }
}
