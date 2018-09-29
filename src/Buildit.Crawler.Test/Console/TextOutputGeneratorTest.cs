using Buildit.Crawler.Console;
using Buildit.Crawler.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Buildit.Crawler.Test.Console
{
    [TestClass]
    public class TextOutputGeneratorTest
    {
        private readonly string DomainUriString = "https://buildit.wiprodigital.com";
        private readonly string _n = Environment.NewLine;

        [TestMethod]
        public void When_NodeHasNoChildren_Then_ReturnOnlyRootNode()
        {
            // Arrange
            var node = CreateNode("/home.html");
            var target = new TextOutputGenerator();

            // Act
            var actual = target.Generate(node);

            //Assert
            var expected = "https://buildit.wiprodigital.com/home.html" + _n;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void When_NodeHasOneChildren_Then_ReturnOnlyOneNode()
        {
            // Arrange
            var node = CreateNode("/");
            node.Nodes = new List<Node>() { CreateNode("/home.html") };
            var target = new TextOutputGenerator();

            // Act
            var actual = target.Generate(node);

            //Assert
            var expected =
                "https://buildit.wiprodigital.com/"              + _n +
                "    https://buildit.wiprodigital.com/home.html" + _n;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void When_NodeHasSeveralChildren_Then_ReturnSeveralChildren()
        {
            // Arrange
            var node = CreateNode("/");
            node.Nodes = new List<Node>() { CreateNode("/home.html"), CreateNode("/about.html") };
            var target = new TextOutputGenerator();

            // Act
            var actual = target.Generate(node);

            //Assert
            var expected =
                "https://buildit.wiprodigital.com/"               + _n +
                "    https://buildit.wiprodigital.com/home.html"  + _n +
                "    https://buildit.wiprodigital.com/about.html" + _n;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void When_NodeHasSeveralNestedChildren_Then_ReturnSeveralNestedChildren()
        {
            // Arrange
            var node = CreateNode("/");
            node.Nodes = new List<Node>() { CreateNode("/home.html"), CreateNode("/about.html"), CreateNode("/contact.html") };
            node.Nodes[0].Nodes = new List<Node>() { CreateNode("/about.html"), CreateNode("/contact.html") };
            node.Nodes[1].Nodes = new List<Node>() { CreateNode("/home.html"), CreateNode("/contact.html") };
            node.Nodes[2].Nodes = new List<Node>() { CreateNode("/home.html"), CreateNode("/about.html") };
            var target = new TextOutputGenerator();

            // Act
            var actual = target.Generate(node);

            //Assert
            var expected =
                "https://buildit.wiprodigital.com/"                     + _n +
                "    https://buildit.wiprodigital.com/home.html"        + _n +
                "        https://buildit.wiprodigital.com/about.html"   + _n +
                "        https://buildit.wiprodigital.com/contact.html" + _n +
                "    https://buildit.wiprodigital.com/about.html"       + _n +
                "        https://buildit.wiprodigital.com/home.html"    + _n +
                "        https://buildit.wiprodigital.com/contact.html" + _n +
                "    https://buildit.wiprodigital.com/contact.html"     + _n +
                "        https://buildit.wiprodigital.com/home.html"    + _n +
                "        https://buildit.wiprodigital.com/about.html"   + _n;
            Assert.AreEqual(expected, actual);
        }

        private Node CreateNode(string linkUriString)
        {
            var result = new Node(new Uri(DomainUriString), new Uri(linkUriString, UriKind.Relative));
            return result;
        }
    }
}