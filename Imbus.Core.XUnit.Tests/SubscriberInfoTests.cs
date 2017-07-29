using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Imbus.Core.XUnit.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class SubscriberInfoTests
    {
        public SubscriberInfoTests()
        {
            m_Sut = new SubscriberInfo <TestMessage>("Test",
                                                     TestAction);
        }

        private readonly SubscriberInfo <TestMessage> m_Sut;
        private bool WasCalled;

        [Fact]
        public void Constructor_Sets_Handler()
        {
            // Arrange
            // Act
            m_Sut.Handler(new TestMessage());

            // Assert
            Assert.True(WasCalled);
        }

        [Fact]
        public void Constructor_Sets_SubscriptionId()
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal("Test",
                         m_Sut.SubscriptionId);
        }

        private void TestAction(TestMessage obj)
        {
            WasCalled = true;
        }

        private class TestMessage
        {
        }
    }
}