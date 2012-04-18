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

namespace PlanetTelex.Common.Models
{
    // ReSharper disable InconsistentNaming
    // These are named this way so they can be displayable with a simple string replacement.

    /// <summary>
    /// Types of credit cards.
    /// </summary>
    public enum CreditCardType
    {
        /// <summary>Unknown = -1</summary>
        [XmlEnum("Unknown")]
        Unknown = -1,
        /// <summary>None = 0</summary>
        [XmlEnum("None")]
        None = 0,
        /// <summary>Visa = 1</summary>
        [XmlEnum("Visa")]
        Visa = 1,
        /// <summary>Mastercard = 2</summary>
        [XmlEnum("Mastercard")]
        Mastercard = 2,
        /// <summary>American_Express = 3</summary>
        [XmlEnum("AmericanExpress")]
        American_Express = 3,
        /// <summary>Discover = 4</summary>
        [XmlEnum("Discover")]
        Discover = 4,
        /// <summary>Diners_Club = 5</summary>
        [XmlEnum("DinersClub")]
        Diners_Club = 5,
        /// <summary>Jcb = 6</summary>
        [XmlEnum("Jcb")]
        Jcb = 6
    }

    // ReSharper restore InconsistentNaming
}
