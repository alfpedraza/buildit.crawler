using Buildit.Crawler.Infrastructure;
using Buildit.Crawler.Service;

namespace Buildit.Crawler.Console
{
    /// <summary>
    /// Represents an object that creates instances of <see cref="Buildit.Crawler.Console.ConsoleCrawlerApp"/>.
    /// </summary>
    public static class ConsoleCrawlerAppFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Buildit.Crawler.Console.ConsoleCrawlerApp"/> class.
        /// </summary>
        /// <returns>A new <see cref="Buildit.Crawler.Console.ConsoleCrawlerApp"/> object created.</returns>
        public static IConsoleApp Create()
        {
            var crawler = new WebCrawler(new NodeFactory(new LinkExtractor()), new SystemHttp());
            var output = new ConsoleOutputHandler(new TextOutputGenerator(), new SystemFile(), new SystemConsole());
            var consoleApp = new ConsoleCrawlerApp(new ConsoleInputHandler(new SystemClock()), output, crawler);
            return consoleApp;
        }
    }
}
