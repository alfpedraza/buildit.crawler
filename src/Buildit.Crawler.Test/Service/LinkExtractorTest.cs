using Buildit.Crawler.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Test.Service
{
    [TestClass]
    public class LinkExtractorTest
    {
        #region Href

        [TestMethod]
        public void When_HrefIsGoodFormatted01_Then_ReturnLinks()
        {
            var actual = RunResults("<a href='link.html'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "link.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted02_Then_ReturnLinks()
        {
            var actual = RunResults("<a href=\"link.html\">");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "link.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted03_Then_ReturnLinks()
        {
            var actual = RunResults("<a href=\"link.html'\">");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "link.html'");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted04_Then_ReturnLinks()
        {
            var actual = RunResults("<a href='link.html\"'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "link.html\"");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted05_Then_ReturnLinks()
        {
            var actual = RunResults("<a id='23' href='link.html' title='3453'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "link.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted06_Then_ReturnLinks()
        {
            var actual = RunResults("<p><a id='23' href='link.html' title='3453'>Link</a></p><p><a id='24' href='home.html' title='3455'>Home</a></p>");
            Assert.AreEqual(actual.Count, 2);
            Assert.AreEqual(actual[0].OriginalString, "link.html");
            Assert.AreEqual(actual[1].OriginalString, "home.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted07_Then_ReturnLinks()
        {
            var actual = RunResults("<a HrEf=\"link.html\">");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "link.html");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted08_Then_ReturnLinks()
        {
            var actual = RunResults("<a href='http://www.google.com/about'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "http://www.google.com/about");
        }

        [TestMethod]
        public void When_HrefIsGoodFormatted09_Then_ReturnLinks()
        {
            var actual = RunResults("<a href='../../about'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "../../about");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted01_Then_NoLinkReturned()
        {
            RunNoResults("<ahref='link.html'>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted02_Then_NoLinkReturned()
        {
            RunNoResults("<href='link.html'>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted03_Then_NoLinkReturned()
        {
            RunNoResults("<a hrf='link.html'>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted04_Then_NoLinkReturned()
        {
            RunNoResults("<a href='link.html>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted05_Then_NoLinkReturned()
        {
            RunNoResults("<a href=link.html'>");
        }

        [TestMethod]
        public void When_HrefIsBadFormatted06_Then_NoLinkReturned()
        {
            RunNoResults("<a href=\"link.html'>");
        }

        #endregion

        #region Src

        [TestMethod]
        public void When_SrcIsGoodFormatted01_Then_ReturnLinks()
        {
            var actual = RunResults("<img src='house.jpg'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "house.jpg");
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted02_Then_ReturnLinks()
        {
            var actual = RunResults("<img src=\"house.jpg\">");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "house.jpg");
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted03_Then_ReturnLinks()
        {
            var actual = RunResults("<img src=\"house.jpg'\">");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "house.jpg'");
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted04_Then_ReturnLinks()
        {
            var actual = RunResults("<img src='house.jpg\"'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "house.jpg\"");
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted05_Then_ReturnLinks()
        {
            var actual = RunResults("<img id='23' src='house.jpg' title='3453'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "house.jpg");
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted06_Then_ReturnLinks()
        {
            var actual = RunResults("<p><img id='23' src='house.jpg' title='3453'>Link</a></p><p><img id='24' src='car.png' title='3455'>Home</a></p>");
            Assert.AreEqual(actual.Count, 2);
            Assert.AreEqual(actual[0].OriginalString, "house.jpg");
            Assert.AreEqual(actual[1].OriginalString, "car.png");
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted07_Then_ReturnLinks()
        {
            var actual = RunResults("<img SrC='house.jpg'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "house.jpg");
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted08_Then_ReturnLinks()
        {
            var actual = RunResults("<img src='http://www.google.com/about/house.jpg'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "http://www.google.com/about/house.jpg");
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted09_Then_ReturnLinks()
        {
            var actual = RunResults("<img src='../../about/house.jpg'>");
            Assert.AreEqual(actual.Count, 1);
            Assert.AreEqual(actual[0].OriginalString, "../../about/house.jpg");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted01_Then_NoLinkReturned()
        {
            RunNoResults("<imgsrc='house.jpg'>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted02_Then_NoLinkReturned()
        {
            RunNoResults("<src='house.jpg'>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted03_Then_NoLinkReturned()
        {
            RunNoResults("<img sfc='house.jpg'>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted04_Then_NoLinkReturned()
        {
            RunNoResults("<img src='house.jpg>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted05_Then_NoLinkReturned()
        {
            RunNoResults("<img src=house.jpg'>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted06_Then_NoLinkReturned()
        {
            RunNoResults("<img src=\"house.jpg'>");
        }

        #endregion

        #region Common

        private List<Uri> RunResults(string html)
        {
            var target = new LinkExtractor();
            var result = target.GetHyperlinks(html);
            return result;
        }

        private void RunNoResults(string html)
        {
            var actual = RunResults(html);
            Assert.IsTrue(actual.Count == 0);
        }

        #endregion
    }
}
