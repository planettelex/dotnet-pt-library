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
﻿namespace PlanetTelex.Web.Protocols.Http
{
    /// <summary>
    /// Common strings used by HTTP.
    /// </summary>
    public static class HttpString
    {
        /// <summary>
        /// "301 Moved Permanently"
        /// </summary>
        public const string STATUS_301 = "301 Moved Permanently";

        /// <summary>
        /// "404 Page Not Found"
        /// </summary>
        public const string STATUS_404 = "404 Page Not Found";

        /// <summary>
        /// "Location"
        /// </summary>
        public const string HEADER_LOCATION = "Location";
    }
}
