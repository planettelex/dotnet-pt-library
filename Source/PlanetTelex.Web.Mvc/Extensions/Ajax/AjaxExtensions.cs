using System.Web.Mvc;
using PlanetTelex.Serialization;

namespace PlanetTelex.Web.Mvc.Extensions.Ajax
{
    /// <summary>
    /// Extension methods to the <see cref="AjaxHelper"/> class.
    /// </summary>
    public static class AjaxExtensions
    {
        private static readonly DataContractJsonSerializer Serializer = new DataContractJsonSerializer();
        
        /// <summary>
        /// Serializes an instance as JSON.
        /// </summary>
        /// <typeparam name="T">The type of object being serialized.</typeparam>
        /// <param name="helper">This AJAX helper.</param>
        /// <param name="instance">The object to serialize.</param>
        /// <returns>A JSON string.</returns>
        public static string JsonSerialize<T>(this AjaxHelper helper, T instance) where T : class
        {
            return helper.JavaScriptStringEncode(Serializer.Serialize(instance));
        }
    }
}
