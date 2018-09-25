using Buildit.Crawler.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Test.Service
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class LinkExtractorSrcTest
    {
        [TestMethod]
        public void When_SrcIsGoodFormatted01_Then_ReturnLinks()
        {
            var result = GetLinks("<img src='house.jpg'>");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("house.jpg", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted02_Then_ReturnLinks()
        {
            var result = GetLinks("<img src=\"house.jpg\">");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("house.jpg", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted03_Then_ReturnLinks()
        {
            var result = GetLinks("<img src=\"house.jpg'\">");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("house.jpg'", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted04_Then_ReturnLinks()
        {
            var result = GetLinks("<img src='house.jpg\"'>");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("house.jpg\"", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted05_Then_ReturnLinks()
        {
            var result = GetLinks("<img id='23' src='house.jpg' title='3453'>");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("house.jpg", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted06_Then_ReturnLinks()
        {
            var result = GetLinks("<p><img id='23' src='house.jpg' title='3453'>Link</a></p><p><img id='24' src='car.png' title='3455'>Home</a></p>");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("house.jpg", result[0].OriginalString);
            Assert.AreEqual("car.png", result[1].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted07_Then_ReturnLinks()
        {
            var result = GetLinks("<img SrC='house.jpg'>");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("house.jpg", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted08_Then_ReturnLinks()
        {
            var result = GetLinks("<img src='http://www.google.com/about/house.jpg'>");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("http://www.google.com/about/house.jpg", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsGoodFormatted09_Then_ReturnLinks()
        {
            var result = GetLinks("<img src='../../about/house.jpg'>");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("../../about/house.jpg", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsDuplicated01_Then_ReturnUniqueSrc()
        {
            var result = GetLinks("<img src='about'><img src='about'><img src='about'>");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("about", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsDuplicated02_Then_ReturnUniqueSrc()
        {
            var result = GetLinks("<img src='about'><a href='about'><img src='about'>");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("about", result[0].OriginalString);
        }

        [TestMethod]
        public void When_SrcIsBadFormatted01_Then_NoLinkReturned()
        {
            AssertNoResults("<imgsrc='house.jpg'>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted02_Then_NoLinkReturned()
        {
            AssertNoResults("<src='house.jpg'>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted03_Then_NoLinkReturned()
        {
            AssertNoResults("<img sfc='house.jpg'>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted04_Then_NoLinkReturned()
        {
            AssertNoResults("<img src='house.jpg>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted05_Then_NoLinkReturned()
        {
            AssertNoResults("<img src=house.jpg'>");
        }

        [TestMethod]
        public void When_SrcIsBadFormatted06_Then_NoLinkReturned()
        {
            AssertNoResults("<img src=\"house.jpg'>");
        }

        private List<Uri> GetLinks(string html)
        {
            var target = new LinkExtractor();
            var result = target.GetHyperlinks(html);
            return result;
        }

        private void AssertNoResults(string html)
        {
            var result = GetLinks(html);
            Assert.AreEqual(0, result.Count);
        }
    }
}
