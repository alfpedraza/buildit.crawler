using Buildit.Crawler.Console;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Buildit.Crawler.Test.Other
{
    [TestClass]
    [TestCategory("SystemTest")]
    public class ConsoleCrawlerAppSystemTest
    {
        [TestMethod]
        public void When_ArgumentsAreOK_Then_CrawlAndSaveFile()
        {
            // Arrange
            var domain = "https://buildit.wiprodigital.com/";
            var filePath = "test.txt";
            var args = new string[] { domain, filePath, "false" };
            System.IO.File.Delete(filePath);

            // Act
            var target = ConsoleCrawlerAppFactory.Create();
            target.Run(args);

            // Assert
            var fileExists = System.IO.File.Exists(filePath);
            var firstLine = System.IO.File.ReadAllLines(filePath)[0];
            Assert.IsTrue(fileExists);
            Assert.AreEqual(domain, firstLine);
        }
    }
}
