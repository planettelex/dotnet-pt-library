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
﻿using System.Xml.Serialization;

namespace PlanetTelex.Web.Protocols.Http
{
    /// <summary>
    /// Service response status message and codes enum.
    /// These are based off of the HTTP standard (http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html)
    /// Messages that don't correspond to the HTTP standard will use the range of Names from 1 to 99.
    /// </summary>
    public enum StatusMessage
    {
        /// <summary>
        /// Unknown = 0
        /// </summary>
        [XmlEnum(Name = "Unknown")]
        Unknown = 0,
        /// <summary>
        /// Ok = 200
        /// </summary>
        [XmlEnum(Name = "OK")]
        Ok = 200,
        /// <summary>
        /// BadRequest = 400
        /// </summary>
        [XmlEnum(Name = "Bad Request")]
        BadRequest = 400,
        /// <summary>
        /// Unauthorized = 401
        /// </summary>
        [XmlEnum(Name = "Unauthorized")]
        Unauthorized = 401,
        /// <summary>
        /// Forbidden = 403
        /// </summary>
        [XmlEnum(Name = "Forbidden")]
        Forbidden = 403,
        /// <summary>
        /// NotFound = 404
        /// </summary>
        [XmlEnum(Name = "Not Found")]
        NotFound = 404
    }
}
