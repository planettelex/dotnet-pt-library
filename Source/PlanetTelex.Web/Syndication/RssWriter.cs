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
using System.IO;
using System.Xml;

namespace PlanetTelex.Web.Syndication
{
    /// <summary>
    /// Writes an RSS feed.
    /// </summary>
    public class RssWriter<T> where T : IRssItem, IRssPodcastItem
    {
        /// <summary>
        /// Generates an RSS XML feed.
        /// </summary>
        /// <param name="rssChannel">A <see cref="RssChannel&lt;T&gt;" />.</param>
        /// <returns>An XML string.</returns>
        public virtual string WriteRss(RssChannel<T> rssChannel) 
        {
            StringWriter returnVal = new StringWriter();
            XmlTextWriter rssWriter = new XmlTextWriter(returnVal) { Formatting = Formatting.Indented };

            rssWriter.WriteStartDocument();
            rssWriter.WriteStartElement("rss");
            rssWriter.WriteAttributeString("version", "2.0");
            rssWriter.WriteStartElement("channel");

            rssWriter.WriteStartElement("title");
            rssWriter.WriteString(rssChannel.Title);
            rssWriter.WriteEndElement();
            rssWriter.WriteStartElement("link");
            rssWriter.WriteString(rssChannel.Link);
            rssWriter.WriteEndElement();
            rssWriter.WriteStartElement("description");
            rssWriter.WriteString(rssChannel.Description);
            rssWriter.WriteEndElement();
            rssWriter.WriteStartElement("language");
            rssWriter.WriteString(rssChannel.Language);
            rssWriter.WriteEndElement();
            rssWriter.WriteStartElement("pubDate");
            rssWriter.WriteString(rssChannel.PublicationDate.ToString("R"));
            rssWriter.WriteEndElement();
            rssWriter.WriteStartElement("lastBuildDate");
            rssWriter.WriteString(rssChannel.LastBuildDate.ToString("R"));
            rssWriter.WriteEndElement();
            rssWriter.WriteStartElement("generator");
            rssWriter.WriteString(rssChannel.Generator);
            rssWriter.WriteEndElement();
            rssWriter.WriteStartElement("managingEditor");
            rssWriter.WriteString(rssChannel.ManagingEditor);
            rssWriter.WriteEndElement();
            rssWriter.WriteStartElement("webMaster");
            rssWriter.WriteString(rssChannel.WebMaster);
            rssWriter.WriteEndElement();

            foreach (T rssItem in rssChannel.Items)
            {
                rssWriter.WriteStartElement("item");
                rssWriter.WriteStartElement("title");
                rssWriter.WriteString(rssItem.RssItemTitle);
                rssWriter.WriteEndElement();
                rssWriter.WriteStartElement("link");
                rssWriter.WriteString(rssItem.RssItemLink);
                rssWriter.WriteEndElement();
                rssWriter.WriteStartElement("description");
                rssWriter.WriteString(rssItem.RssItemDescription);
                rssWriter.WriteEndElement();
                rssWriter.WriteStartElement("author");
                rssWriter.WriteString(rssItem.RssItemAuthor);
                rssWriter.WriteEndElement();
                rssWriter.WriteStartElement("comments");
                rssWriter.WriteString(rssItem.RssItemComments);
                rssWriter.WriteEndElement();
                rssWriter.WriteStartElement("pubDate");
                rssWriter.WriteString(rssItem.RssItemPublishedDate.ToLongDateString());
                rssWriter.WriteEndElement();

                if (typeof(T) == typeof(IRssPodcastItem))
                {
                    rssWriter.WriteStartElement("enclosure");
                    rssWriter.WriteAttributeString("url", rssItem.RssItemEnclosureFileLocationUrl);
                    rssWriter.WriteAttributeString("length", rssItem.RssItemEnclosureFileSize);
                    rssWriter.WriteAttributeString("type", rssItem.RssItemEnclosureFileType);
                    rssWriter.WriteEndElement();
                }
                rssWriter.WriteEndElement(); // End item
            }

            rssWriter.WriteEndElement(); // End channel
            rssWriter.WriteEndElement(); // End rss
            rssWriter.WriteEndDocument();
            rssWriter.Flush();
            rssWriter.Close();
            return returnVal.ToString().Replace("utf-16", "UTF-8");
        }
    }
}