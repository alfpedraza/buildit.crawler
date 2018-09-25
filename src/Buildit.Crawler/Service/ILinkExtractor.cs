using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Service
{
    /// <summary>
    /// Provides a mechanism to extract hyperlinks from HTML.
    /// </summary>
    public interface ILinkExtractor
    {
        /// <summary>
        /// Returns all the hyperlinks contained in the specified HTML text.
        /// </summary>
        /// <param name="html">The HTML text to look for.</param>
        /// <returns>A list of <see cref="System.Uri"/> that represents all the hyperlinks found.</returns>
        List<Uri> GetHyperlinks(string html);
    }
}
