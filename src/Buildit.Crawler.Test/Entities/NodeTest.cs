﻿using Buildit.Crawler.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Buildit.Crawler.Test.Entities
{
    [TestClass]
    public class NodeTest
    {
        [TestMethod]
        public void When_DomainAndLinkUriAreValid_Then_NodeUriIsValid()
        {
            var domainUri = new Uri("https://buildit.wiprodigital.com/");
            var linkUri = new Uri("/about/", UriKind.Relative);
            var target = new Node(domainUri, linkUri);
            var expected = "https://buildit.wiprodigital.com/about/";
            var actual = target.Uri.AbsoluteUri;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void When_DomainUriIsValidAndLinkUriWeird_Then_NodeUriIsValid()
        {
            var domainUri = new Uri("https://buildit.wiprodigital.com/");
            var linkUri = new Uri("about this great test!", UriKind.Relative);
            var target = new Node(domainUri, linkUri);
            var expected = "https://buildit.wiprodigital.com/about%20this%20great%20test!";
            var actual = target.Uri.AbsoluteUri;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void When_DomainAndLinkUriAreSameHost_Then_NodeIsInternal()
        {
            var domainUri = new Uri("https://buildit.wiprodigital.com/");
            var linkUri = new Uri("https://buildit.wiprodigital.com/about");
            var target = new Node(domainUri, linkUri);
            Assert.IsTrue(target.IsInternal);
        }

        [TestMethod]
        public void When_LinkUriIsRelative_Then_NodeIsInternal()
        {
            var domainUri = new Uri("https://buildit.wiprodigital.com/");
            var linkUri = new Uri("/about/", UriKind.Relative);
            var target = new Node(domainUri, linkUri);
            Assert.IsTrue(target.IsInternal);
        }

        [TestMethod]
        public void When_LinkUriHasAnotherHost_Then_NodeIsExternal()
        {
            var domainUri = new Uri("https://buildit.wiprodigital.com/about");
            var linkUri = new Uri("https://www.google.com/about");
            var target = new Node(domainUri, linkUri);
            Assert.IsFalse(target.IsInternal);
        }
    }
}
