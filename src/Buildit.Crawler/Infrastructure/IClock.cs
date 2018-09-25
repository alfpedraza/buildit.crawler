using System;

namespace Buildit.Crawler.Infrastructure
{
    /// <summary>
    /// Represents a device that returns the time.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Returns the current date and time.
        /// </summary>
        DateTime Now { get; }
    }
}
