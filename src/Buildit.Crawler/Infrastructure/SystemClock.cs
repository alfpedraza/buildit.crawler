using System;

namespace Buildit.Crawler.Infrastructure
{
    public class SystemClock : IClock
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
