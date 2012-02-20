using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PlanetTelex.Extensions;
using PlanetTelex.Web.Common;

namespace PlanetTelex.Web.Mvc.Extensions.Html.Lists
{
    /// <summary>
    /// <see cref="HtmlHelper"/> extensions for working with <see cref="SelectList"/> classes.
    /// </summary>
    public static class SelectListExtensions
    {
        /// <summary>
        /// Creates a select list based on the items in a provided IEnumerable class.
        /// </summary>
        /// <typeparam name="T">The type contained in the IEnumerable.</typeparam>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="items">The IEnumerable of items.</param>
        /// <param name="selectedItem">The selected item.</param>
        /// <returns>A <see cref="SelectList"/>.</returns>
        public static SelectList ToSelectList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items, T selectedItem) where T : ISelectable
        {
            if (null == items)
                return new SelectList(new ArrayList());

            var values = (from item in items select new { ID = item.Value, Name = item.Text }).ToList();
            return new SelectList(values, "ID", "Name", selectedItem.Value);
        }

        /// <summary>
        /// Creates a select list based on the items in a provided IEnumerable class.
        /// </summary>
        /// <typeparam name="T">The type contained in the IEnumerable.</typeparam>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="items">The IEnumerable of items.</param>
        /// <param name="selectedItem">The selected item.</param>
        /// <param name="defaultOption">A default option to insert at the top of the <see cref="SelectList"/>.</param>
        /// <returns>A <see cref="SelectList"/>.</returns>
        public static SelectList ToSelectList<T>(this HtmlHelper htmlHelper, IEnumerable<T> items, T selectedItem, string defaultOption) where T : ISelectable
        {
            if (null == items)
                return new SelectList(new ArrayList());

            var values = (from item in items select new { ID = item.Value, Name = item.Text }).ToList();

            if (defaultOption.IsNotNullOrEmpty())
                values.Insert(0, new { ID = (object)"", Name = (object)defaultOption });
            
            return new SelectList(values, "ID", "Name", selectedItem.Value);
        }
    }
}
