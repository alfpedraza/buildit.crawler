using System;

namespace Buildit.Crawler.Infrastructure
{
    /// <summary>
    /// Represents a simplifified view of a HTTP Get Response.
    /// </summary>
    public class HttpGetResponse
    {
        /// <summary>
        /// Indicates that the request was successfully executed.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets the <see cref="System.String"/> representation of the request.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets the last request Uri. (In case of a HTTP Redirection, this value
        /// will be different from the original request Uri)
        /// </summary>
        public Uri RequestUri { get; set; }
    }
}
