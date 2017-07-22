using System;
using System.Diagnostics.CodeAnalysis;
using Moq;
using NLog;
using NUnit.Framework;

namespace Imbus.Core.Autofac.NLog.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class ImbusLoggerAdapterTests
    {
        [SetUp]
        public void Setup()
        {
            m_MockLogger = new Mock <ILogger>();
            m_Sut = new ImbusLoggerAdapter(m_MockLogger.Object);
        }

        private Mock <ILogger> m_MockLogger;
        private ImbusLoggerAdapter m_Sut;

        [Test]
        public void Debug_Calls_Debug_For_String()
        {
            // Arrange
            // Act
            m_Sut.Debug("Text");

            // Assert
            m_MockLogger.Verify(m => m.Debug("Text",
                                             new object[0]),
                                Times.Once);
        }

        [Test]
        public void Debug_Calls_Debug_For_String_And_Args()
        {
            // Arrange
            // Act
            m_Sut.Debug("Text {0}",
                        1);

            // Assert
            m_MockLogger.Verify(m => m.Debug("Text {0}",
                                             new object[]
                                             {
                                                 1
                                             }),
                                Times.Once);
        }

        [Test]
        public void Error_Calls_Error_For_Exception()
        {
            // Arrange
            var exception = new ArgumentException("Text");

            // Act
            m_Sut.Error(exception);

            // Assert
            m_MockLogger.Verify(m => m.Error(exception),
                                Times.Once);
        }

        [Test]
        public void Error_Calls_Error_For_Exception_And_String()
        {
            // Arrange
            var exception = new ArgumentException("Text");

            // Act
            m_Sut.Error(exception,
                        "Text");

            // Assert
            m_MockLogger.Verify(m => m.Error(exception,
                                             "Text",
                                             new object[0]),
                                Times.Once);
        }

        [Test]
        public void Error_Calls_Error_For_Exception_And_String_And_Args()
        {
            // Arrange
            var exception = new ArgumentException("Text");

            // Act
            m_Sut.Error(exception,
                        "Text",
                        1);

            // Assert
            m_MockLogger.Verify(m => m.Error(exception,
                                             "Text",
                                             1),
                                Times.Once);
        }

        [Test]
        public void Error_Calls_Error_For_String()
        {
            // Arrange
            // Act
            m_Sut.Error("Text");

            // Assert
            m_MockLogger.Verify(m => m.Error("Text",
                                             new object[0]),
                                Times.Once);
        }

        [Test]
        public void Error_Calls_Error_For_String_And_Args()
        {
            // Arrange
            // Act
            m_Sut.Error("Text {0}",
                        1);

            // Assert
            m_MockLogger.Verify(m => m.Error("Text {0}",
                                             new object[]
                                             {
                                                 1
                                             }),
                                Times.Once);
        }

        [Test]
        public void Info_Calls_Info_For_String()
        {
            // Arrange
            // Act
            m_Sut.Info("Text");

            // Assert
            m_MockLogger.Verify(m => m.Info("Text",
                                            new object[0]),
                                Times.Once);
        }

        [Test]
        public void Info_Calls_Info_For_String_And_Args()
        {
            // Arrange
            // Act
            m_Sut.Info("Text {0}",
                       1);

            // Assert
            m_MockLogger.Verify(m => m.Info("Text {0}",
                                            new object[]
                                            {
                                                1
                                            }),
                                Times.Once);
        }
    }
}