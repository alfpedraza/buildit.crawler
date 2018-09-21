using Buildit.Crawler.Entities;
using System;

namespace Buildit.Crawler.Service
{
    public interface IWebCrawler
    {
        Node Crawl(Uri domainUri);
    }
}
