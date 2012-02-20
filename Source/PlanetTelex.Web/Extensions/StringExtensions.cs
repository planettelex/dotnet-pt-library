using System.Web;
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
            return s.Replace("\r\n", HtmlString.BR).Replace("\n", HtmlString.BR);
        }
    }
}
