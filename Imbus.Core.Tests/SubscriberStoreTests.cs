using System.Diagnostics.CodeAnalysis;
using Imbus.Core;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;
using Moq;
using NUnit.Framework;

namespace NCQRS.Core.MessageBus.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class SubscriberStoreTests
    {
        [SetUp]
        public void Setup()
        {
            m_Handler = new TestHandler();
            m_Factory = new RepositoryFactory();

            m_Sut = new SubscriberStore(m_Factory.Create);
        }

        private TestHandler m_Handler;
        private RepositoryFactory m_Factory;
        private SubscriberStore m_Sut;

        public class TestHandler
        {
            public void Handle([NotNull] TestMessage message)
            {
            }
        }

        public class TestMessage
        {
        }

        private class RepositoryFactory
        {
            public readonly Mock <ISubscriberRepository> MockASync = new Mock <ISubscriberRepository>();
            public readonly Mock <ISubscriberRepository> MockSync = new Mock <ISubscriberRepository>();

            private int counter;

            public ISubscriberRepository Create()
            {
                counter++;

                return counter == 1
                           ? MockSync.Object
                           : MockASync.Object;
            }
        }

        [Test]
        public void Subscribe_Calls_Subscribe_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Assert
            m_Factory.MockSync.Verify(m => m.Subscribe <TestMessage>("SubscriptionId",
                                                                     m_Handler.Handle));
        }

        [Test]
        public void SubscribeAsync_Calls_Subscribe_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.SubscribeAsync <TestMessage>("SubscriptionId",
                                               m_Handler.Handle);

            // Assert
            m_Factory.MockASync.Verify(m => m.Subscribe <TestMessage>("SubscriptionId",
                                                                      m_Handler.Handle));
        }

        [Test]
        public void Subscribers_Calls_Subscribers_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribers <TestMessage>();

            // Assert
            m_Factory.MockSync.Verify(m => m.Subscribers <TestMessage>());
        }

        [Test]
        public void SubscribersAsync_Calls_SubscribersAsync_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.SubscribersAsync <TestMessage>();

            // Assert
            m_Factory.MockASync.Verify(m => m.Subscribers <TestMessage>());
        }

        [Test]
        public void Unsubscribe_Calls_Unsubscribe_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            m_Factory.MockSync.Verify(m => m.Unsubscribe <TestMessage>("SubscriptionId"));
        }

        [Test]
        public void UnsubscribeAsync_Calls_Unsubscribe_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.UnsubscribeAsync <TestMessage>("SubscriptionId");

            // Assert
            m_Factory.MockASync.Verify(m => m.Unsubscribe <TestMessage>("SubscriptionId"));
        }
    }
}