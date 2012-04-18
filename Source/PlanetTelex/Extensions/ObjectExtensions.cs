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
using PlanetTelex.Utilities;

namespace PlanetTelex.Extensions
{
    /// <summary>
    /// <see cref="object"/> extension methods. See <see cref="ObjectUtility"/> for additional methods not included as extensions.
    /// </summary>
    public static class ObjectExtensions // Its important to limit the extensions to the base object class.
    {
        private static readonly ObjectUtility ObjectUtility = new ObjectUtility();

        #region Clone Method

        /// <summary>
        /// Performs a "deep copy" of a given object, resulting in a new copy of this object in memory.
        /// </summary>
        /// <param name="o">This object.</param>
        /// <returns>A new object with the same type and property values as this object.</returns>
        public static object Clone(this object o)
        {
            return ObjectUtility.Clone(o);
        }

        #endregion

        #region Conversion Methods

        /// <summary>
        /// Converts this object instance into a dictionary.
        /// </summary>
        /// <remarks>This method is useful when starting with an anonymous type.</remarks>
        /// <param name="o">This object.</param>
        /// <returns>A dictionary representation of this object.</returns>
        public static Dictionary<object, object> ToDictionary(this object o)
        {
            return ObjectUtility.ToDictionary(o);
        }

        /// <summary>
        /// Converts this object instance into a dictionary.
        /// </summary>
        /// <remarks>This method is useful when starting with an anonymous type.</remarks>
        /// <param name="o">This object.</param>
        /// <returns>A dictionary representation of this object.</returns>
        public static Dictionary<string, TVal> ToDictionary<TVal>(this object o)
        {
            return ObjectUtility.ToDictionary<TVal>(o);
        }

        #endregion
    }
}