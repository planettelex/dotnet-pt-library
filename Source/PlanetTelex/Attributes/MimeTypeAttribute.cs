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
