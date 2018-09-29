using Buildit.Crawler.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Test.Service
{
    [TestClass]
    public class NodeTest
    {
        private readonly Uri DomainUri  = new Uri("https://buildit.wiprodigital.com");

        [TestMethod]
        public void When_ThereAreNoLinks_Then_ReturnNoNodes()
        {
            // Arrange
            var html = "";
            var linkList = new List<Uri>() { };
            var target = CreateTarget(html, linkList);

            // Act
            var result = target.Create(DomainUri, html);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void When_ThereIsOneLink_Then_ReturnOneNode()
        {
            // Arrange
            var html = "<a href='https://www.google.com'>";
            var linkList = new List<Uri>() { new Uri("https://www.google.com") };
            var target = CreateTarget(html, linkList);

            // Act
            var result = target.Create(DomainUri, html);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("https://www.google.com/", result[0].Uri.AbsoluteUri);
        }

        [TestMethod]
        public void When_ThereAreSeveralUnorderedLinks_Then_ReturnSeveralOrderedNodes()
        {
            // Arrange
            var html =
                "<a href='https://www.google.com'>" +
                "<a href='https://buildit.wiprodigital.com/about'>" +
                "<a href='/login?id=123'>";
            var linkList = new List<Uri>() {
                new Uri("https://www.google.com"),
                new Uri("https://buildit.wiprodigital.com/about"),
                new Uri("/login?id=123", UriKind.Relative)
            };
            var target = CreateTarget(html, linkList);

            // Act
            var result = target.Create(DomainUri, html);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("https://buildit.wiprodigital.com/about", result[0].Uri.AbsoluteUri);
            Assert.AreEqual("https://buildit.wiprodigital.com/login?id=123", result[1].Uri.AbsoluteUri);
            Assert.AreEqual("https://www.google.com/", result[2].Uri.AbsoluteUri);
        }

        private INodeFactory CreateTarget(string html, List<Uri> linkList)
        {
            var extractorMock = new Mock<ILinkExtractor>();
            extractorMock.Setup(m => m.GetHyperlinks(html)).Returns(linkList);
            var target = new NodeFactory(extractorMock.Object);
            return target;
        }
    }
}
