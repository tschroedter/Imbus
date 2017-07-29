using System.Diagnostics.CodeAnalysis;
using Imbus.Core.Interfaces;
using Moq;
using Xunit;

namespace Imbus.Core.XUnit.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class InMemoryMessageBusTests
    {
        public InMemoryMessageBusTests()
        {
            m_Message = new TestMessage();
            m_Handler = new TestHandler();

            m_MockStore = new Mock <ISubscriberStore>();
            m_MockAggregator = new Mock <IMessageAggregator>();

            m_Sut = new InMemoryMessageBus(m_MockStore.Object,
                                           m_MockAggregator.Object);
        }

        private readonly TestHandler m_Handler;

        private readonly TestMessage m_Message;
        private readonly Mock <IMessageAggregator> m_MockAggregator;
        private readonly Mock <ISubscriberStore> m_MockStore;
        private readonly InMemoryMessageBus m_Sut;

        [Fact]
        public void Publish_Calls_Publish_When_Called()
        {
            // Arrange
            // Act
            m_Sut.Publish(m_Message);

            // Assert
            m_MockAggregator.Verify(m => m.Publish(m_MockStore.Object,
                                                   m_Message),
                                    Times.Once);
        }

        [Fact]
        public void PublishAsync_Calls_PublishAsync_When_Called()
        {
            // Arrange
            // Act
            m_Sut.PublishAsync(m_Message);

            // Assert
            m_MockAggregator.Verify(m => m.PublishAsync(m_MockStore.Object,
                                                        m_Message),
                                    Times.Once);
        }

        [Fact]
        public void Subscribe_Calls_Subscribe_When_Called()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Assert
            m_MockStore.Verify(m => m.Subscribe <TestMessage>("SubscriptionId",
                                                              m_Handler.Handle),
                               Times.Once);
        }

        [Fact]
        public void Subscribe_Calls_Subscribe_When_Called_For_Handler()
        {
            // Arrange
            // Act
            m_Sut.Subscribe(m_Handler);

            // Assert
            m_MockStore.Verify(m => m.Subscribe <TestMessage>(m_Handler.SubscriptionId,
                                                              m_Handler.Handle),
                               Times.Once);
        }

        [Fact]
        public void SubscribeAsync_Calls_SubscribeAsync_Whe_Called()
        {
            // Arrange
            // Act
            m_Sut.SubscribeAsync <TestMessage>("SubscriptionId",
                                               m_Handler.Handle);

            // Assert
            m_MockStore.Verify(m => m.SubscribeAsync <TestMessage>("SubscriptionId",
                                                                   m_Handler.Handle),
                               Times.Once);
        }

        [Fact]
        public void SubscribeAsync_Calls_SubscribeAsync_Whe_Called_For_Handler()
        {
            // Arrange
            // Act
            m_Sut.SubscribeAsync(m_Handler);

            // Assert
            m_MockStore.Verify(m => m.SubscribeAsync <TestMessage>(m_Handler.SubscriptionId,
                                                                   m_Handler.Handle),
                               Times.Once);
        }

        public class TestHandler
            : BaseMessageHandler <TestMessage>
        {
            public TestHandler()
                : base("SubscriperId")
            {
            }

            protected override void HandleMessage(TestMessage message)
            {
            }
        }

        public class TestMessage
        {
        }
    }
}