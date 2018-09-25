using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Entities
{
    /// <summary>
    /// Represents an item in the domain navigation hierarchy.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Gets the Uri of this item.
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// Determines whether this Uri is internal in the domain.
        /// </summary>
        public bool IsInternal { get; }

        /// <summary>
        /// Gets and sets a list of the child items of this item.
        /// </summary>
        public List<Node> Nodes { get; set; }

        /// <summary>
        /// Instanciates a new instance of the <see cref="Buildit.Crawler.Entities.Node"/> class.
        /// </summary>
        /// <param name="domainUri">A Uri that represents the current domain.</param>
        /// <param name="linkUri">A hyperlink Uri.</param>
        public Node(Uri domainUri, Uri linkUri)
        {
            Uri = GetAbsoluteUri(domainUri, linkUri);
            IsInternal = IsLinkInternal(domainUri, Uri);
            Nodes = new List<Node>();
        }

        private Uri GetAbsoluteUri(Uri domainUri, Uri linkUri)
        {
            // If the hyperlink is an absolute Uri (e.g. http://www.google.com)
            if (Uri.IsWellFormedUriString(linkUri.OriginalString, UriKind.Absolute))
            {
                return linkUri;
            }
            // Othewise, build an absolute Uri out of the domain and the hyperlink.
            else
            {
                var absoluteUri = new Uri(domainUri, linkUri);
                return absoluteUri;
            }
        }

        // Checks whether both Uris have the same host.
        private bool IsLinkInternal(Uri domainUri, Uri nodeUri)
        {
            var result = (domainUri.Host == nodeUri.Host);
            return result;
        }
    }
}
