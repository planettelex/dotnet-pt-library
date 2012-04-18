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

namespace PlanetTelex.Web.Protocols.Http
{
    /// <summary>
    /// Represents a 404 not found exception.
    /// </summary>
    public class HttpNotFoundException : HttpException
    {
        private const int CODE = 404;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HttpNotFoundException(string message) : base(CODE, message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public HttpNotFoundException(string message, Exception innerException) : base(CODE, message, innerException) { }
    }
}
