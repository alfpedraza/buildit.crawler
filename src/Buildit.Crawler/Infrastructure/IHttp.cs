using System;

namespace Buildit.Crawler.Infrastructure
{
    /// <summary>
    /// Represents a client that sends HTTP requests.
    /// </summary>
    public interface IHttp
    {
        /// <summary>
        /// Sends a HTTP Get request to the specified Uri.
        /// </summary>
        /// <param name="uri">The Uri to request to.</param>
        /// <returns>A <see cref="Buildit.Crawler.Infrastructure.HttpGetResponse"/>
        /// object that represents the response from the server.</returns>
        HttpGetResponse Get(Uri uri);
    }
}
