using System.Collections.Generic;

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
