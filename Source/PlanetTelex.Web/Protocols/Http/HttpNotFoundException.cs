using System;
using System.Web;

namespace PlanetTelex.Web.Protocols.Http
{
    /// <summary>
    /// Represents a 404 not found exception.
    /// </summary>
    public class HttpNotFoundException : HttpException
    {
        private const int CODE = 404;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public HttpNotFoundException(string message) : base(CODE, message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public HttpNotFoundException(string message, Exception innerException) : base(CODE, message, innerException) { }
    }
}
