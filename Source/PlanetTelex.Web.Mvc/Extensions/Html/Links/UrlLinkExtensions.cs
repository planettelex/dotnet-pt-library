using System;
using System.Web.Mvc;
using PlanetTelex.Web.Common;

namespace PlanetTelex.Web.Mvc.Extensions.Html.Links
{
    /// <summary>
    /// <see cref="HtmlHelper"/> extensions for making links out of URL strings.
    /// </summary>
    public static class UrlLinkExtensions
    {
        /// <summary>
        /// Creates a link for the specified URL.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString UrlLink(this HtmlHelper htmlHelper, string url)
        {
            return UrlLink(htmlHelper, url, url);
        }

        /// <summary>
        /// Creates a link for the specified URL.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="linkText">The link text.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString UrlLink(this HtmlHelper htmlHelper, string url, string linkText)
        {
            return UrlLink(htmlHelper, url, linkText, false);
        }

        /// <summary>
        /// Creates a link for the specified URL.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="url">The URL.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="openInNewWindow">If set to <c>true</c> link will have a target='blank' tag.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString UrlLink(this HtmlHelper htmlHelper, string url, string linkText, bool openInNewWindow)
        {
            string attributes = openInNewWindow ? HtmlString.LINK_TARGET_BLANK : String.Empty;
            return MvcHtmlString.Create(String.Format(HtmlString.LINK, url, attributes, linkText));
        }
    }
}
