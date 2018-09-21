using Buildit.Crawler.Infrastructure;
using Buildit.Crawler.Service;

namespace Buildit.Crawler.Console
{
    public static class ConsoleCrawlerAppFactory
    {
        public static IConsoleApp Create()
        {
            var crawler = new WebCrawler(new NodeFactory(new LinkExtractor()), new SystemHttp());
            var output = new ConsoleOutputHandler(new TextOutputGenerator(), new SystemFile(), new SystemConsole());
            var console = new ConsoleCrawlerApp(new ConsoleInputHandler(new SystemClock()), output, crawler);
            return console;
        }
    }
}
