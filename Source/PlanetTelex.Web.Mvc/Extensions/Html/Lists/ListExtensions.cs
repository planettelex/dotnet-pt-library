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
﻿using System.Collections;
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
            StringBuilder stringBuilder = new StringBuilder(HtmlStrings.UNORDERED_LIST);
            
            foreach (var item in items)
                stringBuilder.Append(HtmlStrings.LIST_ITEM.FormatWith(item.ToString()));
            
            stringBuilder.Append(HtmlStrings.UNORDERED_LIST_END);

            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}
