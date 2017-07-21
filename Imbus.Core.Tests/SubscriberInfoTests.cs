using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Imbus.Core.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SubscriberInfoTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new SubscriberInfo <TestMessage>("Test",
                                                     TestAction);
        }

        private SubscriberInfo <TestMessage> m_Sut;
        private bool WasCalled;

        private class TestMessage
        {
        }

        private void TestAction(TestMessage obj)
        {
            WasCalled = true;
        }

        [Test]
        public void Constructor_Sets_Handler()
        {
            // Arrange
            // Act
            m_Sut.Handler(new TestMessage());

            // Assert
            Assert.True(WasCalled);
        }

        [Test]
        public void Constructor_Sets_SubscriptionId()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual("Test",
                            m_Sut.SubscriptionId);
        }
    }
}