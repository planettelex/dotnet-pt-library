using System.Configuration;

namespace PlanetTelex
{
    ///<summary>
    /// Optional settings for the PlanetTelex assembly obtained from application settings configuration.
    ///</summary>
    public class Settings
    {
        /// <summary>
        /// Gets the current settings.
        /// </summary>
        public static Settings Current
        {
            get { return _current = (_current ?? new Settings()); }
        }
        private static Settings _current;

        #region Public Properties

        /// <summary>
        /// Gets the string specifying the date format specified by the AppSettings key "CreditCardDateFormat". Default is "MMyy".
        /// </summary>
        public string CreditCardDateFormatString
        {
            get
            {
                return ConfigurationManager.AppSettings["CreditCardDateFormat"] ?? "MMyy";
            }
        }

        #endregion
    }
}