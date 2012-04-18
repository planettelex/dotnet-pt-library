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
using PlanetTelex.Web.Request;

namespace PlanetTelex.Web.Mvc.Extensions.Html.Links
{
    /// <summary>
    /// <see cref="HtmlHelper"/> extensions for working with links to a mirror site (i.e. it has the same sitemap).
    /// </summary>
    public static class MirrorSiteLinkExtensions
    {
        /// <summary>
        /// Creates a link to a mirror site at the same relative path as the current URL of this site.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="domain">The domain of the mirror site.</param>
        /// <param name="scheme">The scheme of the URL to build.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString MirrorSiteLink(this HtmlHelper htmlHelper, string linkText, string domain, UriScheme scheme)
        {
            string url = scheme.Prefix + domain + "/" + UriParts.Current.UriPath.TrimStart('/');
            if (UriParts.Current.Parameters != null && UriParts.Current.Parameters.Count > 0)
                url += "?" + UriParts.Current.QueryPart;

            return MvcHtmlString.Create(String.Format(HtmlStrings.LINK, url, HtmlStrings.LINK_TARGET_BLANK, linkText));
        }
    }
}