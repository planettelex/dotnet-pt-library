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

namespace PlanetTelex.Collections
{
    /// <summary>
    /// A paginated list.
    /// </summary>
    /// <typeparam name="T">The type listed.</typeparam>
    [Serializable]
    public class PaginatedList<T> : IPaginatedList<T>
    {
        /// <summary>
        /// This list.
        /// </summary>
        protected List<T> List;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList&lt;T&gt;"/> class.
        /// </summary>
        public PaginatedList() : this(new List<T>()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="list">A list to initialize this instance with.</param>
        public PaginatedList(List<T> list)
        {
            if (null == list)
                list = new List<T>();

            List = list;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="list">A list to initialize this instance with.</param>
        /// <param name="totalCount">The total count.</param>
        public PaginatedList(List<T> list, int totalCount)
        {
            List = list;
            TotalCount = totalCount;
        }

        #endregion

        #region IPaginatedList<T> Members

        /// <summary>
        /// Gets or sets the total result count.
        /// </summary>
        /// <value>
        /// The total result count, which may be greater than the count in this list.
        /// </value>
        public int TotalCount { get; set; }

        /// <summary>
        /// Determines the index of a specific item in the list.
        /// </summary>
        /// <param name="item">The object to locate in the list.</param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item)
        {
            return List.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the list at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the list.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the list.
        ///   </exception>
        ///   
        /// <exception cref="T:System.NotSupportedException">
        /// The list is read-only.
        ///   </exception>
        public void Insert(int index, T item)
        {
            List.Insert(index, item);
        }

        /// <summary>
        /// Removes the list item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the list.
        ///   </exception>
        ///   
        /// <exception cref="T:System.NotSupportedException">
        /// The list is read-only.
        ///   </exception>
        public void RemoveAt(int index)
        {
            List.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        /// The element at the specified index.
        ///   </returns>
        ///   
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the list.
        ///   </exception>
        ///   
        /// <exception cref="T:System.NotSupportedException">
        /// The property is set and the list is read-only.
        ///   </exception>
        public T this[int index]
        {
            get { return List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        ///   </exception>
        public void Add(T item)
        {
            List.Add(item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        ///   </exception>
        public void Clear()
        {
            List.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            return List.Contains(item);
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        ///   </returns>
        public int Count
        {
            get { return List.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        ///   </returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        ///   </exception>
        public bool Remove(T item)
        {
            return List.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }

        #endregion
    }
}
