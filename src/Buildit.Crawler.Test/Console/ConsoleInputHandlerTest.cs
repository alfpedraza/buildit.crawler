using Buildit.Crawler.Console;
using Buildit.Crawler.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Buildit.Crawler.Test.Console
{
    [TestClass]
    [TestCategory("UnitTest")]
    public class ConsoleInputHandlerTest
    {
        [TestMethod]
        public void When_NoArgumentsProvided_Then_ReturnDefaultValues()
        {
            // Arrange
            var clockMock = new Mock<IClock>();
            var dateTimeNow = new DateTime(2018, 9, 24, 1, 1, 1);
            clockMock.Setup(m => m.Now).Returns(dateTimeNow);
            var target = new ConsoleInputHandler(clockMock.Object);
            var args = new string[] { };

            // Act
            target.ReadArguments(args);

            //Assert
            var expectedOutputFilePath = string.Format(ConsoleInputHandler.DefaultOutputFilePathFormat, dateTimeNow);
            Assert.AreEqual(new Uri(ConsoleInputHandler.DefaultDomain), target.DomainUri);
            Assert.AreEqual(expectedOutputFilePath, target.OutputFilePath);
            Assert.AreEqual(Convert.ToBoolean(ConsoleInputHandler.DefaultWaitBeforeEnd), target.WaitBeforeEnd);
        }

        [TestMethod]
        public void When_WellFormattedArgumentsProvided_Then_ReturnArgumentValues()
        {
            // Arrange
            var clockMock = new Mock<IClock>();
            var target = new ConsoleInputHandler(clockMock.Object);
            var args = new string[] { "http://www.google.com/", "C:/crawler.txt", "false" };

            // Act
            target.ReadArguments(args);

            //Assert
            Assert.AreEqual(new Uri("http://www.google.com/"), target.DomainUri);
            Assert.AreEqual("C:/crawler.txt", target.OutputFilePath);
            Assert.AreEqual(Convert.ToBoolean("false"), target.WaitBeforeEnd);
        }

        [TestMethod]
        public void When_SomeArgumentsProvided_Then_ReturnArgumentValuesAndDefaultValues()
        {
            // Arrange
            var clockMock = new Mock<IClock>();
            var dateTimeNow = new DateTime(2018, 9, 24, 1, 1, 1);
            clockMock.Setup(m => m.Now).Returns(dateTimeNow);
            var target = new ConsoleInputHandler(clockMock.Object);
            var args = new string[] { "http://www.google.com/" };

            // Act
            target.ReadArguments(args);

            //Assert
            var expectedOutputFilePath = string.Format(ConsoleInputHandler.DefaultOutputFilePathFormat, dateTimeNow);
            Assert.AreEqual(new Uri("http://www.google.com/"), target.DomainUri);
            Assert.AreEqual(expectedOutputFilePath, target.OutputFilePath);
            Assert.AreEqual(Convert.ToBoolean(ConsoleInputHandler.DefaultWaitBeforeEnd), target.WaitBeforeEnd);
        }

        [TestMethod]
        public void When_MoreArgumentsProvided_Then_ReturnArgumentValuesAndIgnoreTheRest()
        {
            // Arrange
            var clockMock = new Mock<IClock>();
            var target = new ConsoleInputHandler(clockMock.Object);
            var args = new string[] { "http://www.google.com/", "C:/crawler.txt", "false" };

            // Act
            target.ReadArguments(args);

            //Assert
            Assert.AreEqual(new Uri("http://www.google.com/"), target.DomainUri);
            Assert.AreEqual("C:/crawler.txt", target.OutputFilePath);
            Assert.AreEqual(Convert.ToBoolean("false"), target.WaitBeforeEnd);
        }

        [TestMethod]
        public void When_DomainArgumentIsProvided_Then_GetOnlyDomainDataFromUri()
        {
            // Arrange
            var clockMock = new Mock<IClock>();
            var target = new ConsoleInputHandler(clockMock.Object);
            var args = new string[] { "http://www.google.com/about" };

            // Act
            target.ReadArguments(args);

            //Assert
            Assert.AreEqual(new Uri("http://www.google.com/"), target.DomainUri);
        }

        [TestMethod]
        public void When_OutputFilePathArgumentIsProvided_Then_ReturnFormattedValue()
        {
            // Arrange
            var clockMock = new Mock<IClock>();
            clockMock.Setup(m => m.Now).Returns(new DateTime(2018, 9, 24));
            var target = new ConsoleInputHandler(clockMock.Object);
            var args = new string[] { "http://www.google.com", "C:/crawler_{0:yyyyMMdd}.txt" };

            // Act
            target.ReadArguments(args);

            //Assert
            Assert.AreEqual("C:/crawler_20180924.txt", target.OutputFilePath);
        }
    }
}
