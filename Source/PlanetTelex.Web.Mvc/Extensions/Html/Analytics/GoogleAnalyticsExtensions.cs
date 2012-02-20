using System;
using System.Web.Mvc;
using PlanetTelex.Web.Mvc.Properties;
using PlanetTelex.Web.Request;

namespace PlanetTelex.Web.Mvc.Extensions.Html.Analytics
{
    /// <summary>
    /// <see cref="HtmlHelper"/> extensions for working with Google Analytics.
    /// </summary>
    public static class GoogleAnalyticsExtensions 
    {
        /// <summary>
        /// Renders the Google Analytics script when on a specified domain.
        /// </summary>
        /// <param name="htmlHelper">This HTML helper.</param>
        /// <param name="analyticsAccount">The analytics account.</param>
        /// <param name="domain">The domain to render on.</param>
        /// <returns>A <see cref="MvcHtmlString"/>.</returns>
        public static MvcHtmlString GoogleAnalyticsScript(this HtmlHelper htmlHelper, string analyticsAccount, string domain)
        {
            return MvcHtmlString.Create(
                String.Compare(domain, UriParts.Current.Domain, StringComparison.OrdinalIgnoreCase) == 0 ? 
                Resources.AnalyticsScript.Replace("[ACCOUNT-NUMBER]", analyticsAccount) : 
                Resources.AnalyticsDomainMismatch);
        }
    }
}