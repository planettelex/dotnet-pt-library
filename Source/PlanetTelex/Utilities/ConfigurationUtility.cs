using System;
using System.Configuration;

namespace PlanetTelex.Utilities
{
    ///<summary>
    /// Utility methods that assist in configuration settings access.
    ///</summary>
    public class ConfigurationUtility
    {
        #region Application Setting Methods

        /// <summary>
        /// Gets the specified application setting.
        /// </summary>
        /// <param name="name">The application setting name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The application setting value.</returns>
        public virtual string GetAppSetting(string name, string defaultValue)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            string returnVal = ConfigurationManager.AppSettings[name];
            if (string.IsNullOrEmpty(returnVal))
                returnVal = defaultValue;
            
            return returnVal;
        }

        /// <summary>
        /// Gets the specified application setting.
        /// </summary>
        /// <param name="name">The application setting name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The application setting value.</returns>
        public virtual bool GetAppSetting(string name, bool defaultValue)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            bool returnVal = defaultValue;
            string value = ConfigurationManager.AppSettings[name];
            if (!string.IsNullOrEmpty(value))
                returnVal = bool.Parse(value);
            
            return returnVal;
        }

        /// <summary>
        /// Gets the specified application setting.
        /// </summary>
        /// <param name="name">The application setting name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The application setting value.</returns>
        public virtual int GetAppSetting(string name, int defaultValue)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            int returnVal = defaultValue;
            string value = ConfigurationManager.AppSettings[name];
            if (!string.IsNullOrEmpty(value))
                returnVal = int.Parse(value);
            
            return returnVal;
        }

        #endregion
    }
}
