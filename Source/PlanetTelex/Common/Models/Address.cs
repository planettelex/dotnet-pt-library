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

namespace PlanetTelex.Common.Models
{
    /// <summary>
    /// A simple mailing address.
    /// </summary>
    [Serializable]
    public class Address
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the first line of the street address.
        /// </summary>
        /// <value>
        /// The first, or primary street address.
        /// </value>
        public string Street1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the street address.
        /// </summary>
        /// <value>
        /// The secondary street address, often used to express unit numbers.
        /// </value>
        public string Street2 { get; set; }

        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        /// <value>
        /// The city name.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the region name.  This is typically a state or province.
        /// </summary>
        /// <value>
        /// The region name. This is typically a state or province.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        /// <value>
        /// The country name.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal, or zip, code.
        /// </summary>
        /// <value>
        /// The postal code. In the US this is the ZIP code.
        /// </value>
        public string PostalCode { get; set; }
    }

        #endregion
}