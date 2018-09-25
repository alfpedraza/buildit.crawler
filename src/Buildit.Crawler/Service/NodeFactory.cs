using Buildit.Crawler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Buildit.Crawler.Service
{
    public class NodeFactory : INodeFactory
    {
        private readonly ILinkExtractor _linkExtractor;

        public NodeFactory(ILinkExtractor linkExtractor)
        {
            _linkExtractor = linkExtractor;
        }

        public List<Node> Create(Uri domainUri, string html)
        {
            // Gets the list of hyperlinks in the specified HTML code.
            var nodeList = new List<Node>();
            var linkList = _linkExtractor.GetHyperlinks(html);

            // Creates a list of nodes.
            foreach (var linkUri in linkList)
            {
                var node = new Node(domainUri, linkUri);
                nodeList.Add(node);
            }

            // Returns a list ordered by AbsoluteUri ascending.
            var result = nodeList.OrderBy(n => n.Uri.AbsoluteUri).ToList();
            return result;
        }
    }
}