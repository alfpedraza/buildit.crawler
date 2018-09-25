using Buildit.Crawler.Entities;
using Buildit.Crawler.Infrastructure;
using Buildit.Crawler.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buildit.Crawler.Test.Service
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class WebCrawlerTest
    {
        #region Helper Members

        private const string DomainBaseUriString = "https://buildit.wiprodigital.com";
        private const string BaseUriString = "/";
        private const string HomeUriString = "/home.html";
        private const string AboutUriString = "/about.html";
        private const string ContactUriString = "/contact.html";
        private readonly Uri DomainBase = new Uri(DomainBaseUriString);
        private readonly Uri BaseUri = new Uri(BaseUriString, UriKind.Relative);
        private readonly Uri HomeUri = new Uri(HomeUriString, UriKind.Relative);
        private readonly Uri AboutUri = new Uri(AboutUriString, UriKind.Relative);
        private readonly Uri ContactUri = new Uri(ContactUriString, UriKind.Relative);
        private readonly Node BaseNode = new Node(new Uri(DomainBaseUriString), new Uri(BaseUriString, UriKind.Relative));
        private readonly Node HomeNode = new Node(new Uri(DomainBaseUriString), new Uri(HomeUriString, UriKind.Relative));
        private readonly Node AboutNode = new Node(new Uri(DomainBaseUriString), new Uri(AboutUriString, UriKind.Relative));
        private readonly Node ContactNode = new Node(new Uri(DomainBaseUriString), new Uri(ContactUriString, UriKind.Relative));

        private Mock<INodeFactory> _factoryMock;
        private Mock<IHttp> _httpMock;

        [TestInitialize]
        public void Initialize()
        {
            _factoryMock = new Mock<INodeFactory>();
            _httpMock = new Mock<IHttp>();
        }

        #endregion

        [TestMethod]
        public void When_DomainIsNotFound_Then_ReturnNoChildrenNodes()
        {
            // Arrange
            ArrangeMocks(DomainBase, false, string.Empty, DomainBase, new List<Node>() { });
            var target = new WebCrawler(_factoryMock.Object, _httpMock.Object);

            //Act
            var result = target.Crawl(DomainBase);

            // Assert
            Assert.AreEqual(0, result.Nodes.Count);
        }

        [TestMethod]
        public void When_HtmlDoesntHaveHyperlinks_Then_ReturnNoChildrenNodes()
        {
            // Arrange
            ArrangeMocks(DomainBase, new List<Node>() { });
            var target = new WebCrawler(_factoryMock.Object, _httpMock.Object);

            //Act
            var result = target.Crawl(DomainBase);

            // Assert
            Assert.AreEqual(0, result.Nodes.Count);
        }

        [TestMethod]
        public void When_HtmlHasOneHyperlinks_Then_ReturnOneChildNode()
        {
            // Arrange
            ArrangeMocks(DomainBase, new List<Node>() { HomeNode });
            ArrangeMocks(HomeNode.Uri, new List<Node>() { });
            var target = new WebCrawler(_factoryMock.Object, _httpMock.Object);

            //Act
            var result = target.Crawl(DomainBase);

            // Assert
            Assert.AreEqual(1, result.Nodes.Count);
            Assert.AreEqual(HomeNode.Uri, result.Nodes[0].Uri);
        }

        [TestMethod]
        public void When_HtmlHasSeveralHyperlinks_Then_ReturnSeveralChildrenNodes()
        {
            // Arrange
            ArrangeMocks(DomainBase, new List<Node>() { HomeNode, AboutNode, ContactNode });
            ArrangeMocks(HomeNode.Uri, new List<Node>() { });
            ArrangeMocks(AboutNode.Uri, new List<Node>() { });
            ArrangeMocks(ContactNode.Uri, new List<Node>() { });
            var target = new WebCrawler(_factoryMock.Object, _httpMock.Object);

            //Act
            var result = target.Crawl(DomainBase);

            // Assert
            Assert.AreEqual(3, result.Nodes.Count);
            Assert.AreEqual(HomeNode.Uri, result.Nodes[0].Uri);
            Assert.AreEqual(AboutNode.Uri, result.Nodes[1].Uri);
            Assert.AreEqual(ContactNode.Uri, result.Nodes[2].Uri);
        }

        [TestMethod]
        public void When_PageContainsVisitedNode_Then_DontRepeatVisitedNode()
        {
            // Arrange
            ArrangeMocks(DomainBase, new List<Node>() { HomeNode, AboutNode });
            ArrangeMocks(HomeNode.Uri, new List<Node>() { BaseNode });
            ArrangeMocks(AboutNode.Uri, new List<Node>() { BaseNode });
            var target = new WebCrawler(_factoryMock.Object, _httpMock.Object);

            //Act
            var result = target.Crawl(DomainBase);

            // Assert
            Assert.AreEqual(2, result.Nodes.Count);
            Assert.AreEqual(HomeNode.Uri, result.Nodes[0].Uri);
            Assert.AreEqual(AboutNode.Uri, result.Nodes[1].Uri);
            Assert.AreEqual(0, result.Nodes[0].Nodes[0].Nodes.Count);
            Assert.AreEqual(0, result.Nodes[1].Nodes[0].Nodes.Count);
        }

        #region Helper Methods

        private void ArrangeMocks(Uri linkUri, List<Node> nodeList)
        {
            var html = GenerateDummyHtml(nodeList);
            ArrangeMocks(linkUri, true, html, linkUri, nodeList);
        }

        private void ArrangeMocks(Uri linkUri, bool isSuccess, string html, Uri requestUri, List<Node> nodeList)
        {
            _httpMock.Setup(m => m.Get(linkUri)).Returns(new HttpGetResponse() { IsSuccess = isSuccess, Content = html, RequestUri = requestUri });
            _factoryMock.Setup(m => m.Create(DomainBase, html)).Returns(nodeList);
        }

        private string GenerateDummyHtml(List<Node> nodeList)
        {
            var builder = new StringBuilder();
            foreach (var node in nodeList)
            {
                builder.AppendFormat("<a href='{0}'>A</a>", node.Uri);
            }
            return builder.ToString();
        }

        #endregion
    }
}
