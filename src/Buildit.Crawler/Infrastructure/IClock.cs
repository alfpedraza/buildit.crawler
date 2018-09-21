using System;

namespace Buildit.Crawler.Infrastructure
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
