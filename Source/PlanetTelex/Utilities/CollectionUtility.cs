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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PlanetTelex.Common;
using PlanetTelex.Properties;

namespace PlanetTelex.Utilities
{
    /// <summary>
    /// Utility methods that assist in collection manipulation.
    /// </summary>
    public class CollectionUtility
    {
        private readonly ObjectUtility _objectUtility = new ObjectUtility();

        #region Removal Methods

        /// <summary>
        /// Removes duplicates from a collection of comparable items.
        /// </summary>
        /// <param name="toRemoveDuplicatesFrom">The collection to remove duplicates from.</param>
        /// <returns>A new collection with unique items.</returns>
        public virtual Collection<T> RemoveCollectionDuplicates<T>(Collection<T> toRemoveDuplicatesFrom)
        {
            Collection<T> newCollection = new Collection<T>();
            foreach (T item in toRemoveDuplicatesFrom)
                if (!newCollection.Contains(item))
                    newCollection.Add(item);

            return newCollection;
        }

        #endregion

        #region Alteration Methods

        /// <summary>
        /// Adds the items in a provided dictionary to a source dictionary.
        /// </summary>
        /// <typeparam name="T">The key data type.</typeparam>
        /// <typeparam name="TV">The value data type.</typeparam>
        /// <param name="source">The dictionary to add to.</param>
        /// <param name="toAdd">The dictionary with items to add.</param>
        /// <param name="overwrite">if set to <c>true</c> overwrite values in the source dictionary with values in the dictionary being added.</param>
        /// <returns>The source dictionary with new items added.</returns>
        public virtual Dictionary<T, TV> AddDictionary<T, TV>(Dictionary<T, TV> source, Dictionary<T, TV> toAdd, bool overwrite)
        {
            foreach (T key in toAdd.Keys)
            {
                if (source.ContainsKey(key))
                {
                    if (overwrite)
                        source[key] = toAdd[key];
                }
                else
                    source.Add(key, toAdd[key]);

            }
            return source;
        }

        #endregion

        #region Sort Methods

        /// <summary>
        /// Sorts an ArrayList of objects by a given property of that class in the specified order.
        /// </summary>
        /// <param name="toSort">The ArrayList to sort.</param>
        /// <param name="objectsType">The type of objects we are sorting.</param>
        /// <param name="sortByProperty">The property to sort by. The data type of this property must be either a primitive type or a type that implements IComparable.</param>
        /// <param name="order">Ascending or descending.</param>
        /// <returns>A new, sorted ArrayList.</returns>
        public virtual ArrayList SortArrayList(ArrayList toSort, Type objectsType, string sortByProperty, Order order)
        {
            if (toSort == null)
                throw new ArgumentNullException("toSort", Resources.SortArgumentNullException2);
            if (objectsType == null)
                throw new ArgumentNullException("toSort", Resources.SortArgumentNullException2);
            if (sortByProperty == null)
                throw new ArgumentNullException("toSort", Resources.SortArgumentNullException2);
            if (!_objectUtility.ContainsProperty(sortByProperty, toSort[0]))
                throw new ArgumentException(string.Format(Resources.SortArgumentException1, objectsType.FullName, sortByProperty));

            if (toSort.Count < 2) return toSort;

            PropertyInfo sortByPropertyInfo = objectsType.GetProperty(sortByProperty);
            Type sortByPropertyInfoType = sortByPropertyInfo.PropertyType;

            if (!_objectUtility.IsComparable(sortByPropertyInfoType))
                throw new ArgumentException(string.Format(Resources.SortArgumentException2, objectsType.FullName, sortByPropertyInfoType.FullName));

            ArrayList propertyValues = new ArrayList();
            foreach (object item in toSort)
            {
                object propertyValue = sortByPropertyInfo.GetValue(item, null);
                if (!propertyValues.Contains(propertyValue))
                    propertyValues.Add(propertyValue);
            }
            propertyValues.Sort();
            if (order == Order.Descending)
                propertyValues.Reverse();

            ArrayList sortedObjects = new ArrayList();
            foreach (object propertyValue in propertyValues)
            {
                if (propertyValue != null)
                {
                    foreach (object item in toSort)
                    {
                        object itemValue = sortByPropertyInfo.GetValue(item, null);
                        if (itemValue.Equals(propertyValue))
                            sortedObjects.Add(item);
                    }
                }
            }
            return sortedObjects;
        }

        /// <summary>
        /// Sorts an ArrayList of objects by a given property of that class in the specified order.
        /// </summary>
        /// <param name="toSort">The ArrayList to sort.</param>
        /// <param name="sortByProperty">The property to sort by. The data type of this property must be either a primative type or a type that implements IComparable.</param>
        /// <param name="order">Ascending or descending.</param>
        /// <returns>A new, sorted ArrayList.</returns>
        public virtual ArrayList SortArrayList(ArrayList toSort, string sortByProperty, Order order)
        {
            if (toSort == null)
                throw new ArgumentNullException("toSort", Resources.SortArgumentNullException1);
            if (sortByProperty == null)
                throw new ArgumentNullException("toSort", Resources.SortArgumentNullException1);

            if (toSort.Count < 2) return toSort;
            
            object item = toSort[0];
            return SortArrayList(toSort, item.GetType(), sortByProperty, order);
        }

        /// <summary>
        /// Sorts an IEnumberable by a given property of their contained class in the specified order.
        /// </summary>
        /// <typeparam name="T">The type of objects in the collection.</typeparam>
        /// <param name="toSort">This IEnumerable.</param>
        /// <param name="sortByProperty">The property to sort by. The data type of this property must be either a primative type or a type that implements IComparable.</param>
        /// <param name="order">Ascending or descending.</param>
        /// <returns>A new, sorted IEnumerable.</returns>
        public virtual IEnumerable<T> SortIEnumberable<T>(IEnumerable<T> toSort, string sortByProperty, Order order)
        {
            // ReSharper disable PossibleMultipleEnumeration
            var param = Expression.Parameter(typeof(T), string.Empty);
            try
            {
                var property = Expression.Property(param, sortByProperty);
                var sortLambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), param);
                return order == Order.Descending ? toSort.AsQueryable().OrderByDescending(sortLambda) : toSort.AsQueryable().OrderBy(sortLambda);
            }
            catch (ArgumentException)
            {
                return toSort;
            }
            // ReSharper restore PossibleMultipleEnumeration
        }

        #endregion

        #region Transform Methods

        /// <summary>
        /// Swaps the keys and values of a given dictionary.
        /// </summary>
        /// <typeparam name="T">The key data type.</typeparam>
        /// <typeparam name="TV">The value data type.</typeparam>
        /// <param name="toSwap">A key-value dictionary to swap the key-value pairs.</param>
        /// <returns>A new dictionary with the key-value pairs interchanged.</returns>
        public virtual Dictionary<TV, T> SwapDictionaryKeysAndValues<T, TV>(Dictionary<T, TV> toSwap)
        {
            return toSwap.Keys.ToDictionary(key => toSwap[key]);
        }

        /// <summary>
        /// Lists the value of the specified property for each item in the ArrayList.
        /// </summary>
        /// <param name="objects">A list of objects.</param>
        /// <param name="propertyName">A property name of the datatype contained in the ArrayList.</param>
        /// <returns>A new ArrayList with the values of the specified property.</returns>
        public virtual ArrayList ArrayListItemPropertyValues(ArrayList objects, string propertyName)
        {
            ArrayList propertyValues = new ArrayList();
            if (objects != null)
            {
                if(objects.Count > 0)
                {
                    PropertyInfo property = _objectUtility.GetProperty(objects[0].GetType(), propertyName);
                    foreach(object obj in objects)
                        propertyValues.Add(property.GetValue(obj, null));
                }
            }
            return propertyValues;
        }

        /// <summary>
        /// Creates a Dictionary that groups items in the given ArrayList by a specified property of the contained items.
        /// </summary>
        /// <param name="objects">A list of objects.</param>
        /// <param name="property">The property to group on.</param>
        /// <returns>An object-ArrayList Dictionary where the values of the provided property are the keys, and the values are the items in the collection sharing that property value.</returns>
        public virtual Dictionary<object, ArrayList> Group(ArrayList objects, string property)
        {
            Dictionary<object, ArrayList> groups = new Dictionary<object, ArrayList>();
            if (objects != null)
            {
                if (objects.Count > 0)
                {
                    PropertyInfo propertyName = _objectUtility.GetProperty(objects[0].GetType(), property);
                    foreach (Object obj in objects)
                    {
                        object value = propertyName.GetValue(obj, null);
                        if (!groups.ContainsKey(value))
                            groups.Add(value, new ArrayList());

                        groups[value].Add(obj);
                    }
                }
            }
            return groups;
        }

        #endregion
    }
}