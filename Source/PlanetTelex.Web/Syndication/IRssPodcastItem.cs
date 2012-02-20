namespace PlanetTelex.Web.Syndication
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
