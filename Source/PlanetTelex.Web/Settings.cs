using System.Configuration;

namespace PlanetTelex.Web
{
    /// <summary>
    /// Application settings for the PlanetTelex.Web assembly.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Gets the current settings.
        /// </summary>
        public static Settings Current
        {
            get { return new Settings(); }
        }
        
        /// <summary>
        /// Gets whether the site should be using cached data specified by the app settings key "CacheEnabled". Default: true
        /// </summary>
        public bool CacheEnabled
        {
            get
            {
                string cacheSetting = ConfigurationManager.AppSettings["CacheEnabled"];
                return cacheSetting == null || bool.Parse(cacheSetting);
            }
        }
    }
}