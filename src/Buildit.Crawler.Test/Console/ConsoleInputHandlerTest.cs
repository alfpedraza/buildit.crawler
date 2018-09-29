using Buildit.Crawler.Console;
using Buildit.Crawler.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Buildit.Crawler.Test.Console
{
    [TestClass]
    public class ConsoleInputHandlerTest
    {
        [TestMethod]
        public void When_NoArgumentsProvided_Then_ReturnDefaultValues()
        {
            // Arrange
            var args = new string[] { };
            var dateTimeNow = new DateTime(2018, 9, 24, 1, 1, 1);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.Now).Returns(dateTimeNow);
            var target = new ConsoleInputHandler(clockMock.Object);

            // Act
            target.ReadArguments(args);

            //Assert
            var expectedOutputFilePath = string.Format(ConsoleInputHandler.DefaultOutputFilePathFormat, dateTimeNow);
            Assert.AreEqual(ConsoleInputHandler.DefaultDomain, target.DomainUri.AbsoluteUri);
            Assert.AreEqual(expectedOutputFilePath, target.OutputFilePath);
            Assert.AreEqual(ConsoleInputHandler.DefaultWaitBeforeEnd, target.WaitBeforeEnd.ToString());
        }

        [TestMethod]
        public void When_WellFormattedArgumentsProvided_Then_ReturnArgumentValues()
        {
            // Arrange
            var args = new string[] { "http://www.google.com/", "C:/crawler.txt", "false" };
            var clockMock = new Mock<IClock>();
            var target = new ConsoleInputHandler(clockMock.Object);

            // Act
            target.ReadArguments(args);

            //Assert
            Assert.AreEqual("http://www.google.com/", target.DomainUri.AbsoluteUri);
            Assert.AreEqual("C:/crawler.txt", target.OutputFilePath);
            Assert.AreEqual("False", target.WaitBeforeEnd.ToString());
        }

        [TestMethod]
        public void When_SomeArgumentsProvided_Then_ReturnArgumentValuesAndDefaultValues()
        {
            // Arrange
            var args = new string[] { "http://www.google.com/" };
            var dateTimeNow = new DateTime(2018, 9, 24, 1, 1, 1);
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.Now).Returns(dateTimeNow);
            var target = new ConsoleInputHandler(clockMock.Object);

            // Act
            target.ReadArguments(args);

            //Assert
            var expectedOutputFilePath = string.Format(ConsoleInputHandler.DefaultOutputFilePathFormat, dateTimeNow);
            Assert.AreEqual("http://www.google.com/", target.DomainUri.AbsoluteUri);
            Assert.AreEqual(expectedOutputFilePath, target.OutputFilePath);
            Assert.AreEqual(ConsoleInputHandler.DefaultWaitBeforeEnd, target.WaitBeforeEnd.ToString());
        }

        [TestMethod]
        public void When_MoreArgumentsProvided_Then_ReturnArgumentValuesAndIgnoreTheRest()
        {
            // Arrange
            var args = new string[] { "http://www.google.com/", "C:/crawler.txt", "false" };
            var clockMock = new Mock<IClock>();
            var target = new ConsoleInputHandler(clockMock.Object);

            // Act
            target.ReadArguments(args);

            //Assert
            Assert.AreEqual("http://www.google.com/", target.DomainUri.AbsoluteUri);
            Assert.AreEqual("C:/crawler.txt", target.OutputFilePath);
            Assert.AreEqual("False", target.WaitBeforeEnd.ToString());
        }

        [TestMethod]
        public void When_DomainArgumentIsProvided_Then_GetOnlyDomainDataFromUri()
        {
            // Arrange
            var args = new string[] { "http://www.google.com/about" };
            var clockMock = new Mock<IClock>();
            var target = new ConsoleInputHandler(clockMock.Object);

            // Act
            target.ReadArguments(args);

            //Assert
            Assert.AreEqual("http://www.google.com/", target.DomainUri.AbsoluteUri);
        }

        [TestMethod]
        public void When_OutputFilePathArgumentIsProvided_Then_ReturnFormattedValue()
        {
            // Arrange
            var args = new string[] { "http://www.google.com", "C:/crawler_{0:yyyyMMdd}.txt" };
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.Now).Returns(new DateTime(2018, 9, 24));
            var target = new ConsoleInputHandler(clockMock.Object);

            // Act
            target.ReadArguments(args);

            //Assert
            Assert.AreEqual("C:/crawler_20180924.txt", target.OutputFilePath);
        }
    }
}
