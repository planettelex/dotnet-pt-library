using System.Xml.Serialization;

namespace PlanetTelex.Common.Models
{
    // ReSharper disable InconsistentNaming
    // These are named this way so they can be displayable with a simple string replacement.

    /// <summary>
    /// Types of credit cards.
    /// </summary>
    public enum CreditCardType
    {
        /// <summary>Unknown = -1</summary>
        [XmlEnum("Unknown")]
        Unknown = -1,
        /// <summary>None = 0</summary>
        [XmlEnum("None")]
        None = 0,
        /// <summary>Visa = 1</summary>
        [XmlEnum("Visa")]
        Visa = 1,
        /// <summary>Mastercard = 2</summary>
        [XmlEnum("Mastercard")]
        Mastercard = 2,
        /// <summary>American_Express = 3</summary>
        [XmlEnum("AmericanExpress")]
        American_Express = 3,
        /// <summary>Discover = 4</summary>
        [XmlEnum("Discover")]
        Discover = 4,
        /// <summary>Diners_Club = 5</summary>
        [XmlEnum("DinersClub")]
        Diners_Club = 5,
        /// <summary>Jcb = 6</summary>
        [XmlEnum("Jcb")]
        Jcb = 6
    }

    // ReSharper restore InconsistentNaming
}
