using System;

namespace PlanetTelex.Attributes
{
    /// <summary>
    /// Field attribute for specifiying a MIME type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MimeTypeAttribute : Attribute
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MimeTypeAttribute"/> class.
        /// </summary>
        /// <param name="value">The MIME type value.</param>
        public MimeTypeAttribute(string value)
        {
            Value = value;
        }

        #endregion

        /// <summary>
        /// The MIME type value.
        /// </summary>
        public string Value { get; set; }
    }
}
