using Buildit.Crawler.Service;

namespace Buildit.Crawler.Console
{
    public class ConsoleCrawlerApp : IConsoleApp
    {
        private readonly IConsoleInputHandler _input;
        private readonly IConsoleOutputHandler _output;
        private readonly IWebCrawler _crawler;

        public ConsoleCrawlerApp(IConsoleInputHandler input, IConsoleOutputHandler output, IWebCrawler crawler)
        {
            _input = input;
            _output = output;
            _crawler = crawler;
        }

        public void Run(string[] args)
        {
            _input.ReadArguments(args);
            var domain = _input.DomainUri;
            var filePath = _input.OutputFilePath;
            var wait = _input.WaitBeforeEnd;
            var result = _crawler.Crawl(domain);
            _output.Write(filePath, result, wait);
        }
    }
}
