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
using System.Xml.Serialization;
using PlanetTelex.Attributes;

namespace PlanetTelex.Common
{
    /// <summary>
    /// Common file extensions with MIME type attributes.
    /// </summary>
    public enum FileExtension
    {
        /// <summary>None = 0</summary>
        None = 0,
        /// <summary>Jpg = 1</summary>
        [XmlEnum("Jpg")]
        [MimeType("image/jpeg")]
        Jpg = 1,
        /// <summary>Gif = 2</summary>
        [XmlEnum("Gif")]
        [MimeType("image/gif")]
        Gif = 2,
        /// <summary>Png = 3</summary>
        [XmlEnum("Png")]
        [MimeType("image/png")]
        Png = 3,
        /// <summary>Tiff = 4</summary>
        [XmlEnum("Tiff")]
        [MimeType("image/tiff")]
        Tiff = 4,
        /// <summary>Bmp = 5</summary>
        [XmlEnum("Bmp")]
        [MimeType("image/bmp")]
        Bmp = 5,
        /// <summary>Threeg2 = 6</summary>
        [XmlEnum("Threeg2")]
        [MimeType("video/3gpp2")]
        Threeg2 = 6,
        /// <summary>Threegp = 7</summary>
        [XmlEnum("Threegp")]
        [MimeType("video/3gpp")]
        Threegp = 7,
        /// <summary>Threegpp = 8</summary>
        [XmlEnum("Threegpp")]
        [MimeType("video/3gpp")]
        Threegpp = 8,
        /// <summary>Asf = 9</summary>
        [XmlEnum("Asf")]
        [MimeType("video/x-ms-asf")]
        Asf = 9,
        /// <summary>Avi = 10</summary>
        [XmlEnum("Avi")]
        [MimeType("video/x-msvideo")]
        Avi = 10,
        /// <summary>Dat = 11</summary>
        [XmlEnum("Dat")]
        [MimeType("video/mpeg")]
        Dat = 11,
        /// <summary>Flv = 12</summary>
        [XmlEnum("Flv")]
        [MimeType("video/x-flv")]
        Flv = 12,
        /// <summary>M4V = 13</summary>
        [XmlEnum("M4V")]
        [MimeType("video/x-m4v")]
        M4V = 13,
        /// <summary>Mkv = 14</summary>
        [XmlEnum("Mkv")]
        [MimeType("video/x-matroska")]
        Mkv = 14,
        /// <summary>Mod = 15</summary>
        [XmlEnum("Mod")]
        [MimeType("video/x-mod")]
        Mod = 15,
        /// <summary>Mov = 16</summary>
        [XmlEnum("Mov")]
        [MimeType("video/quicktime")]
        Mov = 16,
        /// <summary>Mp4 = 17</summary>
        [XmlEnum("Mp4")]
        [MimeType("video/mp4")]
        Mp4 = 17,
        /// <summary>Mpe = 18</summary>
        [XmlEnum("Mpe")]
        [MimeType("video/mpeg")]
        Mpe = 18,
        /// <summary>Mpeg = 19</summary>
        [XmlEnum("Mpeg")]
        [MimeType("video/mpeg")]
        Mpeg = 19,
        /// <summary>Mpeg4 = 20</summary>
        [XmlEnum("Mpeg4")]
        [MimeType("video/mp4")]
        Mpeg4 = 20,
        /// <summary>Mpg = 21</summary>
        [XmlEnum("Mpg")]
        [MimeType("video/mpeg")]
        Mpg = 21,
        /// <summary>Nsv = 22</summary>
        [XmlEnum("Nsv")]
        [MimeType("application/x-winamp")]
        Nsv = 22,
        /// <summary>Ogm = 23</summary>
        [XmlEnum("Ogm")]
        [MimeType("application/ogg")]
        Ogm = 23,
        /// <summary>Ogv = 24</summary>
        [XmlEnum("Ogv")]
        [MimeType("application/ogg")]
        Ogv = 24,
        /// <summary>Qt = 25</summary>
        [XmlEnum("Qt")]
        [MimeType("video/quicktime")]
        Qt = 25,
        /// <summary>Tod = 26</summary>
        [XmlEnum("Tod")]
        [MimeType("video/x-tod")]
        Tod = 26,
        /// <summary>Vob = 27</summary>
        [XmlEnum("Vob")]
        [MimeType("video/dvd")]
        Vob = 27,
        /// <summary>Wmv = 28</summary>
        [XmlEnum("Wmv")]
        [MimeType("video/x-ms-wmv")]
        Wmv = 28,
        /// <summary>Zip = 29</summary>
        [XmlEnum("Zip")]
        [MimeType("application/zip")]
        Zip = 29,
        /// <summary>Asp = 30</summary>
        [XmlEnum("Asp")]
        [MimeType("text/html")]
        Asp = 30,
        /// <summary>Aspx = 31</summary>
        [XmlEnum("Aspx")]
        [MimeType("text/html")]
        Aspx = 31,
        /// <summary>Html = 32</summary>
        [XmlEnum("Html")]
        [MimeType("text/html")]
        Html = 32,
        /// <summary>Pdf = 33</summary>
        [XmlEnum("Pdf")]
        [MimeType("application/pdf")]
        Pdf = 33,
        /// <summary>Config = 34</summary>
        [XmlEnum("Config")]
        [MimeType("text/xml")]
        Config = 34,
        /// <summary>Exe = 35</summary>
        [XmlEnum("Exe")]
        [MimeType("application/octet-stream")]
        Exe = 35,
        /// <summary>Dll = 36</summary>
        [XmlEnum("Dll")]
        [MimeType("application/octet-stream")]
        Dll = 36,
        /// <summary>Dll = 36</summary>
        [XmlEnum("Xml")]
        [MimeType("text/xml")]
        Xml = 36
    }

    /// <summary>
    /// Text encodings.
    /// </summary>
    public enum TextEncoding
    {
        /// <summary>UTF8</summary>
        [XmlEnum("UTF8")]
        Utf8,
        /// <summary>UTF16</summary>
        [XmlEnum("UTF16")]
        Utf16,
        /// <summary>UTF32</summary>
        [XmlEnum("UTF32")]
        Utf32,
        /// <summary>ASCII</summary>
        [XmlEnum("Ascii")]
        Ascii
    }

    /// <summary>
    /// Sorting order of a list.
    /// </summary>
    public enum Order
    {
        /// <summary>Ascending</summary>
        [XmlEnum("Ascending")]
        Ascending,
        /// <summary>Descending</summary>
        [XmlEnum("Descending")]
        Descending
    }

    /// <summary>
    /// Seasons of the year.
    /// </summary>
    [Flags]
    public enum Seasons
    {
        /// <summary>Winter = 1</summary>
        [XmlEnum("Winter")]
        Winter = 1,
        /// <summary>Spring = 2</summary>
        [XmlEnum("Spring")]
        Spring = 2,
        /// <summary>Summer = 4</summary>
        [XmlEnum("Summer")]
        Summer = 4,
        /// <summary>Fall = 8</summary>
        [XmlEnum("Fall")]
        Fall = 8
    }

    /// <summary>
    /// Local or UTC.
    /// </summary>
    public enum TimeMode
    {
        /// <summary>Local</summary>
        [XmlEnum("Local")]
        Local,
        /// <summary>UTC</summary>
        [XmlEnum("UTC")]
        Utc
    }

    /// <summary>
    /// Temperature unit enum.
    /// </summary>
    public enum TemperatureUnit
    {
        /// <summary>C = 1</summary>
        [XmlEnum("C")]
        C = 1,
        /// <summary>F = 2</summary>
        [XmlEnum("F")]
        F = 2
    }
}
