using Buildit.Crawler.Entities;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Service
{
    /// <summary>
    /// Provides a mechanism to create <see cref="Buildit.Crawler.Entities.Node"/> out of some HTML code.
    /// </summary>
    public interface INodeFactory
    {
        /// <summary>
        /// Generates a list of <see cref="Buildit.Crawler.Entities.Node"/> out of the specified HTML code.
        /// </summary>
        /// <param name="domainUri">The domain to be crawled.</param>
        /// <param name="html">The HTML code to look for.</param>
        /// <returns>A list of <see cref="Buildit.Crawler.Entities.Node"/> objects found in the HTML code.</returns>
        List<Node> Create(Uri domainUri, string html);
    }
}
