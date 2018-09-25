using Buildit.Crawler.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Test.Service
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class NodeTest
    {
        private readonly Uri DomainBase  = new Uri("https://buildit.wiprodigital.com");
        private readonly Uri DomainUri1 = new Uri("https://www.google.com");
        private readonly Uri DomainUri2 = new Uri("https://buildit.wiprodigital.com/about");
        private readonly Uri DomainUri3 = new Uri("/login?id=123", UriKind.Relative);

        [TestMethod]
        public void When_ThereAreNoLinks_Then_ReturnNoNodes()
        {
            // Arrange
            var html = string.Empty;
            var linkList = new List<Uri>() { };
            var target = ArrangeTarget(linkList, html);

            // Act
            var result = target.Create(DomainBase, html);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void When_ThereIsOneLink_Then_ReturnOneNode()
        {
            // Arrange
            var html = string.Empty;
            var linkList = new List<Uri>() { DomainUri1 };
            var target = ArrangeTarget(linkList, html);

            // Act
            var result = target.Create(DomainBase, html);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(DomainUri1, result[0].Uri);
        }

        [TestMethod]
        public void When_ThereAreSeveralUnorderedLinks_Then_ReturnSeveralOrderedNodes()
        {
            // Arrange
            var html = string.Empty;
            var linkList = new List<Uri>() { DomainUri2, DomainUri1, DomainUri3 };
            var target = ArrangeTarget(linkList, html);

            // Act
            var result = target.Create(DomainBase, html);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(DomainUri2, result[0].Uri);
            Assert.AreEqual(new Uri(DomainBase, DomainUri3), result[1].Uri);
            Assert.AreEqual(DomainUri1, result[2].Uri);
        }

        private INodeFactory ArrangeTarget(List<Uri> linkList, string html)
        {
            var mock = new Mock<ILinkExtractor>();
            mock.Setup(m => m.GetHyperlinks(html)).Returns(linkList);
            var target = new NodeFactory(mock.Object);
            return target;
        }
    }
}
