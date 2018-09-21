using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Service
{
    public interface ILinkExtractor
    {
        List<Uri> GetHyperlinks(string html);
    }
}
