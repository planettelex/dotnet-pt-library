using System;
using System.Collections.Generic;
using System.Linq;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.Extensions
{
    /// <summary>
    /// IEnumerable extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        private static readonly CollectionUtility CollectionUtility = new CollectionUtility();

        #region Sort Methods

        /// <summary>
        /// Sorts this IEnumberable by a given property of their contained class in the specified order.
        /// </summary>
        /// <typeparam name="T">The type of objects in the collection.</typeparam>
        /// <param name="source">This IEnumerable.</param>
        /// <param name="sortByProperty">The property to sort by. The data type of this property must be either a primative type or a type that implements IComparable.</param>
        /// <param name="order">Ascending or descending.</param>
        /// <returns>A new, sorted IEnumerable.</returns>
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> source, string sortByProperty, Order order)
        {
            return CollectionUtility.SortIEnumberable(source, sortByProperty, order);
        }

        #endregion

        #region Conversion Methods

        /// <summary>
        /// Converts the key-value pairs of this IEnumerable into a formatted string.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TVal">The value type.</typeparam>
        /// <param name="items">This IEnumerable.</param>
        /// <param name="pairFormat">A format string. Defaults to "{0}='{1}'"</param>
        /// <returns>A new string of key-value pairs.</returns>
        public static string ToString<TKey, TVal>(this IEnumerable<KeyValuePair<TKey, TVal>> items, string pairFormat)
        {
            return items.ToString(pairFormat, null);
        }

        /// <summary>
        /// Converts the key-value pairs of this IEnumerable into a formatted string.
        /// </summary>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TVal">The value type.</typeparam>
        /// <param name="items">This IEnumerable.</param>
        /// <param name="pairFormat">A format string. Defaults to "{0}='{1}'"</param>
        /// <param name="separator">The pair separater. Defaults to " "</param>
        /// <returns>A new string of key-value pairs.</returns>
        public static string ToString<TKey, TVal>(this IEnumerable<KeyValuePair<TKey, TVal>> items, string pairFormat, string separator)
        {
            pairFormat = pairFormat ?? "{0}='{1}'";
            separator = separator ?? " ";
            string[] pairs = items.Select(item => String.Format(pairFormat, item.Key.ToString(), item.Value.ToString())).ToArray();
            return String.Join(separator, pairs);
        }

        #endregion

        #region Join Methods

        /// <summary>
        /// Concatenates the items in this instance into a string delimited by ", ".
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="items">This IEnumerable.</param>
        /// <returns>A new concatenated string.</returns>
        public static string Join<T>(this IEnumerable<T> items)
        {
            return items == null ? null : String.Join(", ", items.Select(it => it.ToString()).ToArray());
        }

        /// <summary>
        /// Concatenates the items in this instance into a string delimited by the provided separator.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="items">This IEnumerable.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A new concatenated string.</returns>
        public static string Join<T>(this IEnumerable<T> items, string separator)
        {
            return items == null ? null : String.Join(separator, items.Select(it => it.ToString()).ToArray());
        }

        /// <summary>
        /// Concatenates the items in this instance into a string delimited by ",".
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="items">This IEnumerable.</param>
        /// <param name="selector">A selector to determine the items concatenated.</param>
        /// <returns>A new concatenated string.</returns>
        public static string Join<T>(this IEnumerable<T> items, Func<T, string> selector)
        {
            return items == null ? null : String.Join(", ", items.Select(selector).ToArray());
        }

        /// <summary>
        /// Concatenates the items in this instance into a string delimited by the provided separator.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="items">This IEnumerable.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="selector">A selector to determine the items concatenated.</param>
        /// <returns>A new concatenated string.</returns>
        public static string Join<T>(this IEnumerable<T> items, string separator, Func<T, string> selector)
        {
            return items == null ? null : String.Join(separator, items.Select(selector).ToArray());
        }

        #endregion
    }
}
