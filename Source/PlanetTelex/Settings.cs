/**
 * Copyright (c) 2012 Planet Telex Inc. all rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
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