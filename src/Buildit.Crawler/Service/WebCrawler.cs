using Buildit.Crawler.Entities;
using Buildit.Crawler.Infrastructure;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Service
{
    public class WebCrawler : IWebCrawler
    {
        private readonly Uri BaseUri = new Uri("/", UriKind.RelativeOrAbsolute);

        private readonly INodeFactory _nodeFactory;
        private readonly IHttp _http;
        private readonly HashSet<Uri> _visitedUris;

        public WebCrawler(INodeFactory nodeFactory, IHttp http)
        {
            _nodeFactory = nodeFactory;
            _http = http;
            _visitedUris = new HashSet<Uri>();
        }

        public Node Crawl(Uri domainUri)
        {
            var root = new Node(domainUri, BaseUri);
            CrawlInternal(domainUri, root);
            return root;
        }

        private void CrawlInternal(Uri domainUri, Node node)
        {
            var response = _http.Get(node.Uri);
            SaveVisited(node.Uri);
            SaveVisited(response.RequestUri);
            if (!response.IsSuccess) return;
            
            var html = response.Content;
            node.Nodes = _nodeFactory.Create(domainUri, html);

            foreach (var child in node.Nodes)
            {
                if (child.IsInternal && !IsVisited(child))
                {
                    CrawlInternal(domainUri, child);
                }
            }
        }

        private void SaveVisited(Uri uri)
        {
            _visitedUris.Add(uri);
        }

        private bool IsVisited(Node node)
        {
            return _visitedUris.Contains(node.Uri);
        }
    }
}
