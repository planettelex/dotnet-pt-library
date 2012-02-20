using System;

namespace PlanetTelex.Common.Models
{
    /// <summary>
    /// A basic representation of a person.
    /// </summary>
    [Serializable]
    public class Person
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the person's first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the person's last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the person's email address.
        /// </summary>
        /// <value>
        /// An email address.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the person's primary phone number.
        /// </summary>
        /// <value>
        /// The person's phone number.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the person's cell phone number.
        /// </summary>
        /// <value>
        /// The person's cell phone number.
        /// </value>
        public string CellPhone { get; set; }

        /// <summary>
        /// Gets or sets the person's fax number.
        /// </summary>
        /// <value>
        /// The person's fax number.
        /// </value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the person's <see cref="Address"/>.
        /// </summary>
        /// <value>
        /// The person's street <see cref="Address"/>.
        /// </value>
        public Address Address { get; set; }

        #endregion
    }
}