using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Entities
{
    public class Node
    {
        public Uri Uri { get; }
        public bool IsInternal { get; }
        public List<Node> Nodes { get; set; }

        public Node(Uri domainUri, Uri linkUri)
        {
            Uri = GetAbsoluteUri(domainUri, linkUri);
            IsInternal = IsLinkInternal(domainUri, Uri);
            Nodes = new List<Node>();
        }

        private Uri GetAbsoluteUri(Uri domainUri, Uri linkUri)
        {
            if (Uri.IsWellFormedUriString(linkUri.OriginalString, UriKind.Absolute))
            {
                return linkUri;
            }
            else
            {
                var absoluteUri = new Uri(domainUri, linkUri);
                return absoluteUri;
            }
        }

        private bool IsLinkInternal(Uri domainUri, Uri nodeUri)
        {
            var result = (domainUri.Host == nodeUri.Host);
            return result;
        }
    }
}
