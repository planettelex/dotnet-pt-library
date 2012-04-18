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
﻿using System.Web;
using PlanetTelex.Web.Common;

namespace PlanetTelex.Web.Extensions
{
    /// <summary>
    /// String extension methods with web dependencies.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// HTML encodes this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new HTML encoded string.</returns>
        public static string HtmlEncode(this string s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        /// <summary>
        /// Replaces the newlines with &lt;br&gt; tags in this string.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>A new string with newlines replaced with &lt;br&gt; tags.</returns>
        public static string ReplaceNewlineCharsWithBrTags(this string s)
        {
            return s.Replace("\r\n", HtmlStrings.BR).Replace("\n", HtmlStrings.BR);
        }
    }
}
