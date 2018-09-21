using Buildit.Crawler.Console;

namespace Buildit.Crawler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var consoleApp = ConsoleCrawlerAppFactory.Create();
            consoleApp.Run(args);
        }
    }
}
