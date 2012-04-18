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
using System;

namespace PlanetTelex.Web.Syndication
{
    /// <summary>
    /// An RSS item. An RSS channel will contain a collection of RSS items.
    /// </summary>
    public interface IRssItem
    {
        /// <summary>
        /// Gets the title of this rss item. Example: Venice Film Festival Tries to Quit Sinking
        /// </summary>
        string RssItemTitle { get; }

        /// <summary>
        /// Gets the link of this rss item. Example: http://nytimes.com/2004/12/07FEST.html
        /// </summary>
        string RssItemLink { get; }

        /// <summary>
        /// Gets the description of this rss item. Example: Some of the most heated chatter at the Venice Film Festival this week was about the way that the arrival of the stars at the Palazzo del Cinema was being staged.
        /// </summary>
        string RssItemDescription { get; }

        /// <summary>
        /// Gets the email address of the author of this rss item. Example: author@site.com
        /// </summary>
        string RssItemAuthor { get;  }

        /// <summary>
        /// Gets the URL of a page containing comments about this item. Example: http://www.myblog.org/cgi-local/mt/mt-comments.cgi?entry_id=290
        /// </summary>
        string RssItemComments { get; }

        /// <summary>
        /// Gets the datetime when this item was published. Example(coverted to string): Sun, 19 May 2002 15:21:36 GMT
        /// </summary>
        DateTime RssItemPublishedDate { get; }
    }
}