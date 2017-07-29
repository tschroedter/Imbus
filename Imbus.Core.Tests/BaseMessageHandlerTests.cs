using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Xunit;

namespace Imbus.Core.Tests
{
    [ExcludeFromCodeCoverage]
    public class BaseMessageHandlerTests
    {
        public BaseMessageHandlerTests()
        {
            m_Sut = new TestMessageHandler("Id");
        }

        private class TestMessage
        {
        }

        private class TestMessageHandler
            : BaseMessageHandler<TestMessage>
        {
            public TestMessageHandler([NotNull] string subscriperId)
                : base(subscriperId)
            {
            }

            public bool WasCalled { get; set; }

            protected override void HandleMessage(TestMessage message)
            {
                WasCalled = true;
            }
        }

        private TestMessageHandler m_Sut;

        [Fact]
        public void Constructor_Sets_SubscriptionId()
        {
            // Arrange
            // Act
            // Assert
            Assert.Equal("Ida",
                         m_Sut.SubscriptionId);
        }

        [Fact]
        public void Handle_Calls_HandleMessage()
        {
            // Arrange
            // Act
            m_Sut.Handle(new TestMessage());

            // Assert
            Assert.True(m_Sut.WasCalled);
        }
    }
}