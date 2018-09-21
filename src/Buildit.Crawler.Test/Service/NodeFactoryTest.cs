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
        private readonly Uri DomainUri01 = new Uri("https://buildit.wiprodigital.com");
        private readonly Uri DomainUri02 = new Uri("https://www.google.com");
        private readonly Uri DomainUri03 = new Uri("/login?id=123", UriKind.Relative);

        [TestMethod]
        public void When_NoLinks_Then_ReturnNoNodes()
        {
            var linkList = new List<Uri>() { };
            var target = CreateTarget(linkList);
            var actual = target.Create(DomainUri01, string.Empty);
            Assert.AreEqual(actual.Count, 0);
        }

        [TestMethod]
        public void When_OneLink_Then_ReturnOneNode()
        {
            var linkList = new List<Uri>() { DomainUri01 };
            var target = CreateTarget(linkList);
            var actual = target.Create(DomainUri01, string.Empty);
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].Uri, DomainUri01);
        }

        [TestMethod]
        public void When_SeveralLinksUnordered_Then_ReturnSeveralNodesOrdered()
        {
            var linkList = new List<Uri>() { DomainUri02, DomainUri01, DomainUri03 };
            var target = CreateTarget(linkList);
            var actual = target.Create(DomainUri01, string.Empty);
            Assert.AreEqual(actual.Count, 3);
            Assert.AreEqual(actual[0].Uri, DomainUri01);
            Assert.AreEqual(actual[1].Uri, new Uri(DomainUri01, DomainUri03));
            Assert.AreEqual(actual[2].Uri, DomainUri02);
        }

        public INodeFactory CreateTarget(List<Uri> linkList)
        {
            var mock = new Mock<ILinkExtractor>();
            mock.Setup(m => m.GetHyperlinks(It.IsAny<string>())).Returns(linkList);
            var target = new NodeFactory(mock.Object);
            return target;
        }
    }
}
