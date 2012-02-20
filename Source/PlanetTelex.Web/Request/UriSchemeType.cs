using System.Xml.Serialization;

namespace PlanetTelex.Web.Request
{
    /// <summary>
    /// Common protocols for data transfer over the internet.
    /// </summary>
    public enum UriSchemeType
    {
        /// <summary>None</summary>
        [XmlEnum("None")]
        None,
        /// <summary>Http</summary>
        [XmlEnum("Http")]
        Http,
        /// <summary>Https</summary>
        [XmlEnum("Https")]
        Https,
        /// <summary>Ftp</summary>
        [XmlEnum("Ftp")]
        Ftp,
        /// <summary>mailto</summary>
        [XmlEnum("Mailto")]
        Mailto,
        /// <summary>Protocol is unknown</summary>
        [XmlEnum("Unknown")]
        Unknown
    }
}
