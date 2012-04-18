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
﻿namespace PlanetTelex.Web.Common
{
    /// <summary>
    /// Common HTML string literals, sometimes with values to format dynamically.
    /// </summary>
    public static class HtmlStrings
    {
        /// <summary>
        /// &lt;br/&gt;
        /// </summary>
        public const string BR = "<br/>";

        /// <summary>
        /// &amp;nbsp;
        /// </summary>
        public const string NBSP = "&nbsp;";

        /// <summary>
        /// &amp;quot;
        /// </summary>
        public const string QUOT = "&quot;";

        /// <summary>
        /// &lt;strong&gt;
        /// </summary>
        public const string STRONG = "<strong>";

        /// <summary>
        /// &lt;/strong&gt;
        /// </summary>
        public const string STRONG_END = "</strong>";

        /// <summary>
        /// &lt;ul&gt;
        /// </summary>
        public const string UNORDERED_LIST = "<ul>";

        /// <summary>
        /// &lt;/ul&gt;
        /// </summary>
        public const string UNORDERED_LIST_END = "</ul>";

        /// <summary>
        /// &lt;li&gt;{0}&lt;/li&gt;
        /// </summary>
        public const string LIST_ITEM = "<li>{0}</li>";

        /// <summary>
        /// &lt;a href='{0}' {1}&gt;{2}&lt;/a&gt;
        /// </summary>
        public const string LINK = "<a href='{0}' {1}>{2}</a>";

        /// <summary>
        /// target='_blank'
        /// </summary>
        public const string LINK_TARGET_BLANK = "target='_blank' ";

        /// <summary>
        /// &lt;script type='text/javascript' language='javascript' src='{0}'&gt;&lt;/script&gt;
        /// </summary>
        public const string JAVASCRIPT_INCLUDE = "<script type='text/javascript' language='javascript' src='{0}'></script>";

        /// <summary>
        /// &lt;link type='text/css' rel='stylesheet' href='{0}' /&gt;
        /// </summary>
        public const string CSS_FILE_INCLUDE = "<link type='text/css' rel='stylesheet' href='{0}'/>";

        /// <summary>
        /// &lt;!--[if lt IE {0}]&gt;
        /// </summary>
        public const string LEGACY_IE_CONDITIONAL_START = "<!--[if lt IE {0}]>";

        /// <summary>
        /// &lt;![endif]--&gt;
        /// </summary>
        public const string LEGACY_IE_CONDITIONAL_STOP = "<![endif]-->";
    }
}
