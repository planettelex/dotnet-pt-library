using System;

namespace PlanetTelex.Common.Models
{
    /// <summary>
    /// A simple mailing address.
    /// </summary>
    [Serializable]
    public class Address
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the first line of the street address.
        /// </summary>
        /// <value>
        /// The first, or primary street address.
        /// </value>
        public string Street1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the street address.
        /// </summary>
        /// <value>
        /// The secondary street address, often used to express unit numbers.
        /// </value>
        public string Street2 { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        /// <value>
        /// The city name.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the region name.  This is typically a state or province.
        /// </summary>
        /// <value>
        /// The region name. This is typically a state or province.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        /// <value>
        /// The country name.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal, or zip, code.
        /// </summary>
        /// <value>
        /// The postal code. In the US this is the ZIP code.
        /// </value>
        public string PostalCode { get; set; }
    }

        #endregion
}