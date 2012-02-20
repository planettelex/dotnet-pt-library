using System.Collections;
using System.Text;
using System.Web.Mvc;
using PlanetTelex.Extensions;
using PlanetTelex.Web.Common;

namespace PlanetTelex.Web.Mvc.Extensions.Html.Lists
{
    /// <summary>
    /// <see cref="HtmlHelper"/> extensions for working with HTML lists.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Creates an unordered list based on the items in a provided IEnumerable class.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="items">The IEnumerable of items.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString ToHtmlList(this HtmlHelper htmlHelper, IEnumerable items)
        {
            StringBuilder stringBuilder = new StringBuilder(HtmlString.UNORDERED_LIST);
            
            foreach (var item in items)
                stringBuilder.Append(HtmlString.LIST_ITEM.FormatWith(item.ToString()));
            
            stringBuilder.Append(HtmlString.UNORDERED_LIST_END);

            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}
