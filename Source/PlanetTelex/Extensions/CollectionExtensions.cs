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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.Extensions
{
    /// <summary>
    /// Extension methods for various collection classes.
    /// </summary>
    public static class CollectionExtensions
    {
        private static readonly CollectionUtility CollectionUtility = new CollectionUtility();

        #region Removal Methods

        /// <summary>
        /// Removes duplicates from a collection of comparable items.
        /// </summary>
        /// <param name="source">This collection.</param>
        /// <returns>A new collection with unique items.</returns>
        public static Collection<T> RemoveDuplicates<T>(this Collection<T> source)
        {
            return CollectionUtility.RemoveCollectionDuplicates(source);
        }

        #endregion

        #region Alteration Methods

        /// <summary>
        /// Adds the items in a provided dictionary to this dictionary.
        /// </summary>
        /// <typeparam name="T">The key data type.</typeparam>
        /// <typeparam name="TV">The value data type.</typeparam>
        /// <param name="source">This dictionary.</param>
        /// <param name="toAdd">The dictionary with items to add.</param>
        /// <param name="overwrite">if set to <c>true</c> overwrite values in the source dictionary with values in the dictionary being added.</param>
        /// <returns>This dictionary with new items added.</returns>
        public static Dictionary<T, TV> Add<T, TV>(this Dictionary<T, TV> source, Dictionary<T, TV> toAdd, bool overwrite)
        {
            return CollectionUtility.AddDictionary(source, toAdd, overwrite);
        }

        #endregion

        #region Sort Methods

        /// <summary>
        /// Sorts this ArrayList by a given property of their contained class in the specified order.
        /// </summary>
        /// <param name="source">This ArrayList.</param>
        /// <param name="objectsType">The type of objects we are sorting.</param>
        /// <param name="sortByProperty">The property to sort by. The data type of this property must be either a primative type or a type that implements IComparable.</param>
        /// <param name="order">Ascending or descending.</param>
        /// <returns>A sorted ArrayList.</returns>
        public static ArrayList Sort(this ArrayList source, Type objectsType, string sortByProperty, Order order)
        {
            return CollectionUtility.SortArrayList(source, sortByProperty, order);
        }

        /// <summary>
        /// Sorts this ArrayList by a given property of their contained class in the specified order.
        /// </summary>
        /// <param name="source">This ArrayList.</param>
        /// <param name="sortByProperty">The property to sort by. The data type of this property must be either a primative type or a type that implements IComparable.</param>
        /// <param name="order">Ascending or descending.</param>
        /// <returns>A sorted ArrayList.</returns>
        public static ArrayList Sort(this ArrayList source, string sortByProperty, Order order)
        {
            return CollectionUtility.SortArrayList(source, sortByProperty, order);
        }

        #endregion

        #region Transform Methods

        /// <summary>
        /// Swaps the keys and values of this dictionary.
        /// </summary>
        /// <typeparam name="T">The key data type.</typeparam>
        /// <typeparam name="TV">The value data type.</typeparam>
        /// <param name="source">This dictionary.</param>
        /// <returns>A new dictionary with the key-value pairs interchanged.</returns>
        public static Dictionary<TV, T> SwapKeysAndValues<T, TV>(this Dictionary<T, TV> source)
        {
            return CollectionUtility.SwapDictionaryKeysAndValues(source);
        }

        /// <summary>
        /// Lists the value of the specified property for each item in this ArrayList.
        /// </summary>
        /// <param name="source">This ArrayList.</param>
        /// <param name="propertyName">A property name of a datatype of the provided objects.</param>
        public static ArrayList ListPropertyValues(this ArrayList source, string propertyName)
        {
            return CollectionUtility.ArrayListItemPropertyValues(source, propertyName);
        }

        /// <summary>
        /// Creates a Dictionary that groups items in this ArrayList by a specified property of the contained items.
        /// </summary>
        /// <param name="source">This dictionary.</param>
        /// <param name="property">The property to group on.</param>
        /// <returns>An object-ArrayList Dictionary where the values of the provided property are the keys, and the values are the items in the collection sharing that property value.</returns>
        public static Dictionary<object, ArrayList> Group(this ArrayList source, string property)
        {
            return CollectionUtility.Group(source, property);
        }

        #endregion
    }
}