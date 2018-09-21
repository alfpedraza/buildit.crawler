using Buildit.Crawler.Entities;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Service
{
    public interface INodeFactory
    {
        List<Node> Create(Uri domainUri, string html);
    }
}
