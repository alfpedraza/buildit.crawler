using System;

namespace Buildit.Crawler.Console
{
    /// <summary>
    /// Defines properties to read from the command line arguments.
    /// </summary>
    public interface IConsoleInputHandler
    {
        /// <summary>
        /// Reads the values of the command line arguments.
        /// </summary>
        /// <param name="args">An array of strings that represents the command line arguments.</param>
        void ReadArguments(string[] args);

        /// <summary>
        /// Gets the Uri of the domain to be crawled.
        /// </summary>
        Uri DomainUri { get; }

        /// <summary>
        /// Gets the path to the generated output file.
        /// </summary>
        string OutputFilePath { get; }

        /// <summary>
        /// Determines whether the crawl will wait for a key press at the end.
        /// </summary>
        bool WaitBeforeEnd { get; }
    }
}
