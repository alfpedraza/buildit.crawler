using Buildit.Crawler.Console;
using Buildit.Crawler.Infrastructure;
using Buildit.Crawler.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Buildit.Crawler.Test.Console
{
    [TestClass]
    public class ConsoleCrawlerAppIntegrationTest
    {
        #region Helper Members
        
        private readonly Uri BaseUri = new Uri("http://www.google.com/");
        private readonly Uri HomeUri = new Uri("http://www.google.com/home.html");
        private readonly Uri ContactUri = new Uri("http://www.google.com/contact.html");
        private readonly Uri AboutUri = new Uri("http://www.google.com/about.html");
        private readonly Uri HouseUri = new Uri("http://www.google.com/house.jpg");
        private readonly Uri CarUri = new Uri("http://www.google.com/car.jpg");

        private Mock<IHttp> _httpMock;
        private Mock<IFile> _fileMock;
        private Mock<IConsole> _consoleMock;
        private Mock<IClock> _clockMock;

        [TestInitialize]
        public void Initialize()
        {
            _httpMock = new Mock<IHttp>();
            _fileMock = new Mock<IFile>();
            _consoleMock = new Mock<IConsole>();
            _clockMock = new Mock<IClock>();
        }

        #endregion

        [TestMethod]
        public void When_OnlyOnePageExists_ReturnSimpleOutput()
        {
            // Arrange
            var homeHtml = "<a href='home.html'><a href='contact.html'><img src='house.jpg'><img src='car.jpg'>";
            var fileText = string.Empty;
            var consoleText = string.Empty;
            _clockMock.Setup(m => m.Now).Returns(new DateTime(2018, 9, 24));
            _httpMock.Setup(m => m.Get(BaseUri)).Returns(new HttpGetResponse() { Content = homeHtml, IsSuccess = true, RequestUri = BaseUri });
            _httpMock.Setup(m => m.Get(HomeUri)).Returns(new HttpGetResponse() { Content = string.Empty, IsSuccess = true, RequestUri = HomeUri });
            _httpMock.Setup(m => m.Get(ContactUri)).Returns(new HttpGetResponse() { Content = string.Empty, IsSuccess = true, RequestUri = ContactUri });
            _httpMock.Setup(m => m.Get(HouseUri)).Returns(new HttpGetResponse() { Content = string.Empty, IsSuccess = true, RequestUri = HouseUri });
            _httpMock.Setup(m => m.Get(CarUri)).Returns(new HttpGetResponse() { Content = string.Empty, IsSuccess = true, RequestUri = CarUri });
            _fileMock.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((f, t) =>  fileText = t);
            _consoleMock.Setup(m => m.Write(It.IsAny<string>())).Callback<string>(t => consoleText = t);
            var target = CreateTarget();

            // Act
            var args = new string[] { "http://www.google.com", "C:/test.txt", "false" };
            target.Run(args);

            // Assert
            var expected =
                "http://www.google.com/\r\n" +
                "    http://www.google.com/car.jpg\r\n" +
                "    http://www.google.com/contact.html\r\n" +
                "    http://www.google.com/home.html\r\n" +
                "    http://www.google.com/house.jpg\r\n";
            Assert.AreEqual(expected, fileText);
            Assert.AreEqual(fileText, consoleText);
        }

        [TestMethod]
        public void When_SeveralNestedPagesExists_ReturnNestedOutput()
        {
            // Arrange
            var homeHtml = "<a href='about.html'><a href='contact.html'>";
            var contactHtml = "<a href='home.html'><a href='about.html'>";
            var aboutHtml = "<a href='home.html'><a href='contact.html'>";
            var fileText = string.Empty;
            var consoleText = string.Empty;
            _clockMock.Setup(m => m.Now).Returns(new DateTime(2018, 9, 24));
            _httpMock.Setup(m => m.Get(BaseUri)).Returns(new HttpGetResponse() { Content = homeHtml, IsSuccess = true, RequestUri = HomeUri });
            _httpMock.Setup(m => m.Get(HomeUri)).Returns(new HttpGetResponse() { Content = homeHtml, IsSuccess = true, RequestUri = HomeUri });
            _httpMock.Setup(m => m.Get(ContactUri)).Returns(new HttpGetResponse() { Content = contactHtml, IsSuccess = true, RequestUri = ContactUri });
            _httpMock.Setup(m => m.Get(AboutUri)).Returns(new HttpGetResponse() { Content = aboutHtml, IsSuccess = true, RequestUri = AboutUri });
            _fileMock.Setup(m => m.Write(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((f, t) => fileText = t);
            _consoleMock.Setup(m => m.Write(It.IsAny<string>())).Callback<string>(t => consoleText = t);
            var target = CreateTarget();

            // Act
            var args = new string[] { "http://www.google.com", "C:/test.txt", "false" };
            target.Run(args);

            // Assert
            var expected =
                "http://www.google.com/\r\n" +
                "    http://www.google.com/about.html\r\n" +
                "        http://www.google.com/contact.html\r\n" +
                "            http://www.google.com/about.html\r\n" +
                "            http://www.google.com/home.html\r\n" +
                "        http://www.google.com/home.html\r\n" +
                "    http://www.google.com/contact.html\r\n";
            Assert.AreEqual(expected, fileText);
            Assert.AreEqual(fileText, consoleText);
        }

        #region Helper Methods

        private ConsoleCrawlerApp CreateTarget()
        {
            var crawler = new WebCrawler(new NodeFactory(new LinkExtractor()), _httpMock.Object);
            var output = new ConsoleOutputHandler(new TextOutputGenerator(), _fileMock.Object, _consoleMock.Object);
            var consoleApp = new ConsoleCrawlerApp(new ConsoleInputHandler(_clockMock.Object), output, crawler);
            return consoleApp;
        }

        #endregion



        //[TestMethod]
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