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
﻿using System;
using System.Web;

namespace PlanetTelex.Web.Request
{
    /// <summary>
    /// Class for using URI scheme strings.
    /// </summary>
    public class UriScheme
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UriScheme"/> class using the current web context.
        /// </summary>
        public UriScheme()
        {
            _prefix = HttpContext.Current.Request.Url.Scheme.ToLower() + DELIMITER;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriScheme"/> class.
        /// </summary>
        /// <param name="uriSchemeType">Type of the URI scheme.</param>
        public UriScheme(UriSchemeType uriSchemeType)
        {
            switch (uriSchemeType)
            {
                case UriSchemeType.Http:
                    _prefix = HTTP + DELIMITER;
                    break;
                case UriSchemeType.Https:
                    _prefix = HTTPS + DELIMITER;
                    break;
                case UriSchemeType.Ftp:
                    _prefix = FTP + DELIMITER;
                    break;
                case UriSchemeType.Mailto:
                    _prefix = MAILTO + ":";
                    break;
                default:
                    _prefix = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriScheme"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public UriScheme(Uri uri)
        {
            _prefix = uri.Scheme.ToLower() + DELIMITER;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriScheme"/> class.
        /// </summary>
        /// <param name="uriString">The URI string.</param>
        public UriScheme(string uriString)
        {
            Uri uri = new Uri(uriString);
            _prefix = uri.Scheme.ToLower() + DELIMITER;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the prefix string.
        /// </summary>
        public string Prefix
        {
            get { return _prefix; }
        }
        private readonly string _prefix;

        /// <summary>
        /// Gets the type of the scheme.
        /// </summary>
        /// <value>
        /// The type of the scheme.
        /// </value>
        public UriSchemeType SchemeType
        {
            get
            {
                if (_prefix == string.Empty)
                    return UriSchemeType.None;
                if (_prefix == HTTP + DELIMITER)
                    return UriSchemeType.Http;
                if (_prefix == HTTPS + DELIMITER)
                    return UriSchemeType.Https;
                if (_prefix == FTP + DELIMITER)
                    return UriSchemeType.Ftp;
                if (_prefix == MAILTO + ":")
                    return UriSchemeType.Mailto;

                return UriSchemeType.Unknown;
            }
        }

        #endregion

        #region Public Constants

        /// <summary>
        /// ://
        /// </summary>
        public const string DELIMITER = "://";

        /// <summary>
        /// http
        /// </summary>
        public const string HTTP = "http";

        /// <summary>
        /// https
        /// </summary>
        public const string HTTPS = "https";

        /// <summary>
        /// ftp
        /// </summary>
        public const string FTP = "ftp";

        /// <summary>
        /// mailto
        /// </summary>
        public const string MAILTO = "mailto";

        #endregion
    }
}