using Buildit.Crawler.Console;

namespace Buildit.Crawler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Executes the console crawler application.
            var consoleApp = ConsoleCrawlerAppFactory.Create();
            consoleApp.Run(args);
        }
    }
}
