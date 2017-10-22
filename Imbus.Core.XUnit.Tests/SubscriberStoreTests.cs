using System.Diagnostics.CodeAnalysis;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace Imbus.Core.XUnit.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class SubscriberStoreTests
    {
        public SubscriberStoreTests()
        {
            m_Handler = new TestHandler();

            m_MockSyncRepository = new Mock <ISubscriberRepository>();
            m_MockAsyncRepository = new Mock <ISubscriberRepository>();

            m_Sut = new SubscriberStore(m_MockSyncRepository.Object,
                                        m_MockAsyncRepository.Object);
        }

        private readonly TestHandler m_Handler;

        private readonly Mock <ISubscriberRepository> m_MockAsyncRepository;
        private readonly Mock <ISubscriberRepository> m_MockSyncRepository;
        private readonly SubscriberStore m_Sut;

        [Fact]
        public void Subscribe_Calls_Subscribe_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Assert
            m_MockSyncRepository.Verify(m => m.Subscribe <TestMessage>("SubscriptionId",
                                                                       m_Handler.Handle));
        }

        [Fact]
        public void SubscribeAsync_Calls_Subscribe_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.SubscribeAsync <TestMessage>("SubscriptionId",
                                               m_Handler.Handle);

            // Assert
            m_MockAsyncRepository.Verify(m => m.Subscribe <TestMessage>("SubscriptionId",
                                                                        m_Handler.Handle));
        }

        [Fact]
        public void Subscribers_Calls_Subscribers_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribers <TestMessage>();

            // Assert
            m_MockSyncRepository.Verify(m => m.Subscribers <TestMessage>());
        }

        [Fact]
        public void SubscribersAsync_Calls_SubscribersAsync_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.SubscribersAsync <TestMessage>();

            // Assert
            m_MockAsyncRepository.Verify(m => m.Subscribers <TestMessage>());
        }

        [Fact]
        public void Unsubscribe_Calls_Unsubscribe_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            m_MockSyncRepository.Verify(m => m.Unsubscribe <TestMessage>("SubscriptionId"));
        }

        [Fact]
        public void UnsubscribeAsync_Calls_Unsubscribe_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.UnsubscribeAsync <TestMessage>("SubscriptionId");

            // Assert
            m_MockAsyncRepository.Verify(m => m.Unsubscribe <TestMessage>("SubscriptionId"));
        }

        public class TestHandler
        {
            public void Handle([NotNull] TestMessage message)
            {
            }
        }

        public class TestMessage
        {
        }
    }
}