namespace PlanetTelex.Web.Common
{
    /// <summary>
    /// Defines a contract for a select list item.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Gets the value of a select list item for this instance.
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Gets the text of a select list item for this instance..
        /// </summary>
        object Text { get; }
    }
}
