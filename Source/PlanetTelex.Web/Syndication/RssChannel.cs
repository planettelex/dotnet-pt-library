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
using System.Collections.Generic;

namespace PlanetTelex.Web.Syndication
{
    /// <summary>
    /// An RSS channel.
    /// </summary>
    public class RssChannel<T> where T : IRssItem, IRssPodcastItem
    {
        /// <summary>
        /// Gets or sets the text for the title element.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the text for the link element.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the text for the description element.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the text for the language element.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the date for the pubDate element.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Gets or sets the date for the lastBuildDate element.
        /// </summary>
        public DateTime LastBuildDate { get; set; }

        /// <summary>
        /// Gets or sets the text for the copyright element.
        /// </summary>
        public string Copyright { get; set; }

        /// <summary>
        /// Gets or sets the text for the generator element.
        /// </summary>
        public string Generator { get; set; }

        /// <summary>
        /// Gets or sets the text for the managingEditor element.
        /// </summary>
        public string ManagingEditor { get; set; }

        /// <summary>
        /// Gets or sets the text for the webMaster element.
        /// </summary>
        public string WebMaster { get; set; }

        /// <summary>
        /// Gets or sets a list of items for this channel.
        /// </summary>
        public List<T> Items { get; set; }
    }
}