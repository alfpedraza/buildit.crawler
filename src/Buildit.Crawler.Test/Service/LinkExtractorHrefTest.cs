using Buildit.Crawler.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Test.Service
{
    [TestClass]
    public class LinkExtractorHrefTest
    {
        [TestMethod]
        public void When_HrefIsGoodFormatted01_Then_ReturnLinks()
        {
             AssertLink("<a href='link.html'>", "link.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted02_Then_ReturnLinks()
        {
            AssertLink("<a href=\"link.html\">", "link.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted03_Then_ReturnLinks()
        {
            AssertLink("<a href=\"link.html'\">", "link.html'");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted04_Then_ReturnLinks()
        {
            AssertLink("<a href='link.html\"'>", "link.html\"");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted05_Then_ReturnLinks()
        {
            AssertLink("<a id='23' href='link.html' title='3453'>", "link.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted06_Then_ReturnLinks()
        {
            var result = GetLinks("<p><a id='23' href='link.html' title='3453'>Link</a></p><p><a id='24' href='home.html' title='3455'>Home</a></p>");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("link.html", result[0].OriginalString);
            Assert.AreEqual("home.html", result[1].OriginalString);
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted07_Then_ReturnLinks()
        {
            AssertLink("<a HrEf=\"link.html\">", "link.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted08_Then_ReturnLinks()
        {
            AssertLink("<a href='http://www.google.com/about'>", "http://www.google.com/about");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted09_Then_ReturnLinks()
        {
            AssertLink("<a href='../../about'>", "../../about");
        }

        [TestMethod]
        public void When_HrefIsDuplicated01_Then_ReturnUniqueHref()
        {
            AssertLink("<a href='about'><a href='about'><a href='about'>", "about");
        }

        [TestMethod]
        public void When_HrefIsDuplicated02_Then_ReturnUniqueHref()
        {
            AssertLink("<a href='about'><img src='about'><a href='about'>", "about");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted01_Then_NoLinkReturned()
        {
            AssertNoLinks("<ahref='link.html'>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted02_Then_NoLinkReturned()
        {
            AssertNoLinks("<href='link.html'>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted03_Then_NoLinkReturned()
        {
            AssertNoLinks("<a hrf='link.html'>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted04_Then_NoLinkReturned()
        {
            AssertNoLinks("<a href='link.html>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted05_Then_NoLinkReturned()
        {
            AssertNoLinks("<a href=link.html'>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted06_Then_NoLinkReturned()
        {
            AssertNoLinks("<a href=\"link.html'>");
        }

        private List<Uri> GetLinks(string html)
        {
            var target = new LinkExtractor();
            var result = target.GetHyperlinks(html);
            return result;
        }

        private void AssertLink(string html, string linkUriString)
        {
            var result = GetLinks(html);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(linkUriString, result[0].OriginalString);
        }

        private void AssertNoLinks(string html)
        {
            var result = GetLinks(html);
            Assert.AreEqual(0, result.Count);
        }
    }
}
