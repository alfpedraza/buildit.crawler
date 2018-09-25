using Buildit.Crawler.Entities;
using System;

namespace Buildit.Crawler.Service
{
    /// <summary>
    /// Represents a process that finds hyperlinks recursively out of a domain.
    /// </summary>
    public interface IWebCrawler
    {
        /// <summary>
        /// Crawls the specified domain recursively to find all its nodes.
        /// </summary>
        /// <param name="domainUri">The domain to crawl.</param>
        /// <returns>A root <see cref="Buildit.Crawler.Entities.Node"/> that represents
        /// the hierarchical search tree.</returns>
        Node Crawl(Uri domainUri);
    }
}
