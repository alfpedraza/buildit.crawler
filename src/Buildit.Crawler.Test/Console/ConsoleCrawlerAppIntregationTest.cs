using Buildit.Crawler.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Buildit.Crawler.Test.Console
{
    [TestClass]
    public class ConsoleCrawlerAppIntegrationTest
    {
        [TestMethod]
        public void When_HrefIsGoodFormatted01_Then_ReturnLinks()
        {
            var args = new string[] { "https://buildit.wiprodigital.com", "C:\\crawler.txt", "false"};
            System.IO.File.Delete(args[1]);

            var consoleApp = ConsoleCrawlerAppFactory.Create();
            consoleApp.Run(args);

            var condition = System.IO.File.Exists(args[1]);
            Assert.IsTrue(condition);
        }
    }
}