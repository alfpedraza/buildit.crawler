using System;

namespace Buildit.Crawler.Infrastructure
{
    public class HttpGetResponse
    {
        public bool IsSuccess { get; set; }
        public string Content { get; set; }
        public Uri RequestUri { get; set; }
    }
}
