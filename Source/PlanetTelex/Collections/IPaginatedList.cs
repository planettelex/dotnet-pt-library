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
﻿using System.Collections.Generic;

namespace PlanetTelex.Collections
{
    /// <summary>
    ///  Defines a contract for a paginated list.
    /// </summary>
    /// <typeparam name="T">The type contained in the list.</typeparam>
    public interface IPaginatedList<T> : IList<T>
    {
        /// <summary>
        /// Gets or sets the total result count.
        /// </summary>
        /// <value>
        /// The total result count, which may be greater than the count in this list.
        /// </value>
        int TotalCount { get; set; }
    }
}
