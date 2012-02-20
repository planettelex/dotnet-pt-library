using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PlanetTelex.Web.Mvc.Results
{
    /// <summary>
    /// Outputs a stream through the HTTP response and allows tracking when the stream writing is complete.
    /// </summary>
    public class StreamTrackingResult : FileStreamResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamTrackingResult"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="onWriteComplete">The delegate invoked when the writing is complete.</param>
        public StreamTrackingResult(Stream stream, string contentType, Action onWriteComplete) : base(stream, contentType)
        {
            OnWriteComplete = onWriteComplete;
        }

        /// <summary>
        /// Gets the on write complete action.
        /// </summary>
        public Action OnWriteComplete { get; private set; }

        /// <summary>
        /// Writes the file to the response.
        /// </summary>
        /// <param name="response">The response.</param>
        protected override void WriteFile(HttpResponseBase response)
        {
            base.WriteFile(response);
            OnWriteComplete();
        }
    }
}
