using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using PlanetTelex.Web.Mvc.Properties;

namespace PlanetTelex.Web.Mvc.Extensions.Html.Lists
{
    /// <summary>
    /// <see cref="HtmlHelper"/> extensions generating time drop down lists.
    /// </summary>
    public static class TimeDropDownListExtensions
    {
        /// <summary>
        /// Creates a drop down list of hours needed for non-24 hour clocks (i.e. the numbers 0-12).
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="name">The drop down list name.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString HourDropDownList(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            IEnumerable<SelectListItem> items = Enumerable.Range(0, 13).Select(it => new SelectListItem 
                { 
                    Text = it.ToString("D2"), 
                    Value = it.ToString(CultureInfo.InvariantCulture) 
                });

            return htmlHelper.DropDownList(name, items, htmlAttributes);
        }

        /// <summary>
        /// Creates a drop down list of minutes in an hour (i.e. the numbers 0-60).
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="name">The drop down list name.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString MinuteDropDownList(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            IEnumerable<SelectListItem> items = Enumerable.Range(0, 60).Select(it => new SelectListItem
                {
                    Text = it.ToString("D2"),
                    Value = it.ToString(CultureInfo.InvariantCulture)
                });

            return htmlHelper.DropDownList(name, items, htmlAttributes);
        }

        /// <summary>
        /// Creates a drop down list with resourced AM and PM designators.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="name">The drop down list name.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString AmPmDropDownList(this HtmlHelper htmlHelper, string name, object htmlAttributes)
        {
            IEnumerable<SelectListItem> items = new List<SelectListItem> 
                {
                    new SelectListItem { Text = Resources.AM },
                    new SelectListItem { Text = Resources.PM }
                };

            return htmlHelper.DropDownList(name, items, htmlAttributes);
        }
    }
}
