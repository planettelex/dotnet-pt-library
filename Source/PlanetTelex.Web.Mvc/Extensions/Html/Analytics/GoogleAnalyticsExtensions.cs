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
using PlanetTelex.Web.Mvc.Properties;
using PlanetTelex.Web.Request;

namespace PlanetTelex.Web.Mvc.Extensions.Html.Analytics
{
    /// <summary>
    /// <see cref="HtmlHelper"/> extensions for working with Google Analytics.
    /// </summary>
    public static class GoogleAnalyticsExtensions 
    {
        /// <summary>
        /// Renders the Google Analytics script when on a specified domain.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="analyticsAccount">The analytics account.</param>
        /// <param name="domain">The domain to render on.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString GoogleAnalyticsScript(this HtmlHelper htmlHelper, string analyticsAccount, string domain)
        {
            return MvcHtmlString.Create(
                String.Compare(domain, UriParts.Current.Domain, StringComparison.OrdinalIgnoreCase) == 0 ? 
                Resources.AnalyticsScript.Replace("[ACCOUNT-NUMBER]", analyticsAccount) : 
                Resources.AnalyticsDomainMismatch);
        }
    }
}