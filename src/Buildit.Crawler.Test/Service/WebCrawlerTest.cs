using Buildit.Crawler.Entities;
using Buildit.Crawler.Infrastructure;
using Buildit.Crawler.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Test.Service
{
    [TestClass]
    public class WebCrawlerTest
    {
        [TestMethod]
        public void When_ValidLinks_Then_ReturnValidNodes()
        {
            var domainUri = new Uri("https://buildit.wiprodigital.com");
            var homeUri = new Uri(domainUri, "/home");
            var aboutUri = new Uri(domainUri, "/about");
            var contactUri = new Uri(domainUri, "/contact");

            var returnList = new List<TestHttpReturn>()
            {
                new TestHttpReturn() {
                    Uri = domainUri,
                    Response = new HttpGetResponse()
                    {
                       Content = "<p>HOME</p><a href='/about'>About</a><a href='/contact'>Contact</a>",
                       IsSuccess = true,
                       RequestUri = homeUri
                    },
                    ListNode = new List<Node>()
                    {
                        new Node(domainUri, new Uri("/about", UriKind.Relative)),
                        new Node(domainUri, new Uri("/contact", UriKind.Relative))
                    }
                },
                new TestHttpReturn() {
                    Uri = aboutUri,
                    Response = new HttpGetResponse()
                    {
                       Content = "<p>ABOUT</p><a href='/home'>Home</a><a href='/contact'>Contact</a>",
                       IsSuccess = true,
                       RequestUri = aboutUri
                    },
                    ListNode = new List<Node>()
                    {
                        new Node(domainUri, new Uri("/home", UriKind.Relative)),
                        new Node(domainUri, new Uri("/contact", UriKind.Relative))
                    }
                },
                new TestHttpReturn() {
                    Uri = contactUri,
                    Response = new HttpGetResponse()
                    {
                       Content = "<p>CONTACT</p><a href='/home'>Home</a><a href='/about'>About</a>",
                       IsSuccess = true,
                       RequestUri = contactUri
                    },
                    ListNode = new List<Node>()
                    {
                        new Node(domainUri, new Uri("/home", UriKind.Relative)),
                        new Node(domainUri, new Uri("/about", UriKind.Relative))
                    }
                },
            };
            var target = CreateTarget(returnList);
            var actual = target.Crawl(domainUri);

            Assert.AreEqual(actual.Uri, domainUri);
            Assert.AreEqual(actual.Nodes[0].Uri, aboutUri);
            Assert.AreEqual(actual.Nodes[0].Nodes[0].Uri, homeUri);
            Assert.AreEqual(actual.Nodes[0].Nodes[1].Uri, contactUri);
            Assert.AreEqual(actual.Nodes[0].Nodes[1].Nodes[0].Uri, homeUri);
            Assert.AreEqual(actual.Nodes[0].Nodes[1].Nodes[1].Uri, aboutUri);
            Assert.AreEqual(actual.Nodes[1].Uri, contactUri);
        }

        [TestMethod]
        public void When_InvalidResponse_Then_ReturnNoNode()
        {
            var domainUri = new Uri("https://buildit.wiprodigital.com");

            var factoryMock = new Mock<INodeFactory>();
            var httpMock = new Mock<IHttp>();
            var response = new HttpGetResponse() { IsSuccess = false };
            httpMock.Setup(m => m.Get(domainUri)).Returns(response);

            var target = new WebCrawler(factoryMock.Object, httpMock.Object);
            var actual = target.Crawl(domainUri);

            Assert.AreEqual(actual.Uri, domainUri);
            Assert.AreEqual(actual.Nodes.Count, 0);
        }

        private IWebCrawler CreateTarget(List<TestHttpReturn> returnList)
        {
            var factoryMock = new Mock<INodeFactory>();
            var httpMock = new Mock<IHttp>();

            foreach (var item in returnList)
            {
                factoryMock.Setup(m => m.Create(It.IsAny<Uri>(), item.Response.Content)).Returns(item.ListNode);
                httpMock.Setup(m => m.Get(item.Uri)).Returns(item.Response);
            }

            var target = new WebCrawler(factoryMock.Object, httpMock.Object);
            return target;
        }

        private class TestHttpReturn
        {
            public Uri Uri { get; set; }
            public HttpGetResponse Response { get; set; }
            public List<Node> ListNode { get; set; }
        }
    }
}
