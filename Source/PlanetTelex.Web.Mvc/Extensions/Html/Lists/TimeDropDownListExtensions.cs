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
