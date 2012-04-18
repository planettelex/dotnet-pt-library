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
using System.Web.Mvc;
using PlanetTelex.Web.Common;

namespace PlanetTelex.Web.Mvc.Extensions.Html.Links
{
    /// <summary>
    /// <see cref="HtmlHelper"/> extensions for making links out of URL strings.
    /// </summary>
    public static class UrlLinkExtensions
    {
        /// <summary>
        /// Creates a link for the specified URL.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString UrlLink(this HtmlHelper htmlHelper, string url)
        {
            return UrlLink(htmlHelper, url, url);
        }

        /// <summary>
        /// Creates a link for the specified URL.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="linkText">The link text.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString UrlLink(this HtmlHelper htmlHelper, string url, string linkText)
        {
            return UrlLink(htmlHelper, url, linkText, false);
        }

        /// <summary>
        /// Creates a link for the specified URL.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="openInNewWindow">If set to <c>true</c> link will have a target='blank' tag.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString UrlLink(this HtmlHelper htmlHelper, string url, string linkText, bool openInNewWindow)
        {
            string attributes = openInNewWindow ? HtmlStrings.LINK_TARGET_BLANK : String.Empty;
            return MvcHtmlString.Create(String.Format(HtmlStrings.LINK, url, attributes, linkText));
        }
    }
}
