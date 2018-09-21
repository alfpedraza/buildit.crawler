using System;

namespace Buildit.Crawler.Infrastructure
{
    public interface IHttp
    {
        HttpGetResponse Get(Uri uri);
    }
}
