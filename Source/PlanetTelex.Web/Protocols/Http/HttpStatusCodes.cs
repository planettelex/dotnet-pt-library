using System.Xml.Serialization;

namespace PlanetTelex.Web.Protocols.Http
{
    /// <summary>
    /// Service response status message and codes enum.
    /// These are based off of the HTTP standard (http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html)
    /// Messages that don't correspond to the HTTP standard will use the range of Names from 1 to 99.
    /// </summary>
    public enum StatusMessage
    {
        /// <summary>
        /// Unknown = 0
        /// </summary>
        [XmlEnum(Name = "Unknown")]
        Unknown = 0,
        /// <summary>
        /// Ok = 200
        /// </summary>
        [XmlEnum(Name = "OK")]
        Ok = 200,
        /// <summary>
        /// BadRequest = 400
        /// </summary>
        [XmlEnum(Name = "Bad Request")]
        BadRequest = 400,
        /// <summary>
        /// Unauthorized = 401
        /// </summary>
        [XmlEnum(Name = "Unauthorized")]
        Unauthorized = 401,
        /// <summary>
        /// Forbidden = 403
        /// </summary>
        [XmlEnum(Name = "Forbidden")]
        Forbidden = 403,
        /// <summary>
        /// NotFound = 404
        /// </summary>
        [XmlEnum(Name = "Not Found")]
        NotFound = 404
    }
}
