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
﻿namespace PlanetTelex.Web.Syndication
{
    /// <summary>
    /// An RSS item for Podcast feeds. A podcast RSS channel will contain a collection of RSS items, these items need the enclosure node and it's attributes.
    /// </summary>
    public interface IRssPodcastItem : IRssItem
    {
        /// <summary>
        /// Gets the Url for a media file.
        /// </summary>
        string RssItemEnclosureFileLocationUrl { get; }

        /// <summary>
        /// Gets the File Size for the media file.
        /// </summary>
        string RssItemEnclosureFileSize { get; }

        /// <summary>
        /// Gets the File Type for the media file.
        /// </summary>
        string RssItemEnclosureFileType { get; }
    }
}
