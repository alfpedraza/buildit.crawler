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
            var rootNode = new Node(domainUri, BaseUri);
            CrawlInternal(domainUri, rootNode);
            return rootNode;
        }

        private void CrawlInternal(Uri domainUri, Node node)
        {
            // Gets the response and saves the visisted Uri.
            var response = _http.Get(node.Uri);
            SaveVisited(node.Uri);
            SaveVisited(response.RequestUri);
            if (!response.IsSuccess) return;
            
            // Gets the nodes out of the reponse HTML.
            var html = response.Content;
            node.Nodes = _nodeFactory.Create(domainUri, html);

            // Iterates through the children nodes recursively
            // only if the node is internal and it has not been visited yet.
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
