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
using System;

namespace PlanetTelex.Common.Models
{
    /// <summary>
    /// A basic representation of a person.
    /// </summary>
    [Serializable]
    public class Person
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the person's first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the person's last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the person's email address.
        /// </summary>
        /// <value>
        /// An email address.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the person's primary phone number.
        /// </summary>
        /// <value>
        /// The person's phone number.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the person's cell phone number.
        /// </summary>
        /// <value>
        /// The person's cell phone number.
        /// </value>
        public string CellPhone { get; set; }

        /// <summary>
        /// Gets or sets the person's fax number.
        /// </summary>
        /// <value>
        /// The person's fax number.
        /// </value>
        public string Fax { get; set; }

        /// <summary>
        /// Gets or sets the person's <see cref="Address"/>.
        /// </summary>
        /// <value>
        /// The person's street <see cref="Address"/>.
        /// </value>
        public Address Address { get; set; }

        #endregion
    }
}