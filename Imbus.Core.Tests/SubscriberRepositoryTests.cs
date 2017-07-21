using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Imbus.Core;
using Imbus.Core.Interfaces;
using NUnit.Framework;

namespace NCQRS.Core.MessageBus.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class SubscriberRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            m_Handler = new TestActionHandler();

            m_Sut = new SubscriberRepository();
        }

        private TestActionHandler m_Handler;
        private SubscriberRepository m_Sut;

        private class TestActionHandler
        {
            public void Handle(TestMessage mesage)
            {
            }
        }

        private class TestActionHandlerOther
        {
            public void Handle(TestMessage mesage)
            {
            }
        }

        private class TestMessage
        {
        }

        public class TestMessageOther
        {
        }

        [Test]
        public void Subscribe_AddsIdsToIds_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId 1",
                                          m_Handler.Handle);
            m_Sut.Subscribe <TestMessage>("SubscriptionId 2",
                                          m_Handler.Handle);

            // Assert
            string[] actual = m_Sut.GetSubscriptionIdsForMessage <TestMessage>()
                                   .ToArray();

            Assert.True(actual.Contains("SubscriptionId 1"));
            Assert.True(actual.Contains("SubscriptionId 2"));
        }

        [Test]
        public void Subscribe_AddsIdToIds_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Assert
            IEnumerable <string> actual = m_Sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.True(actual.Contains("SubscriptionId"));
        }

        [Test]
        public void Subscribe_AddsMessageToMessages_ForSameMessageRegistraion()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("Test 1",
                                          m_Handler.Handle);
            m_Sut.Subscribe <TestMessage>("Test 2",
                                          m_Handler.Handle);

            // Assert
            Assert.True(m_Sut.Messages.Contains(typeof( TestMessage )));
        }

        [Test]
        public void Subscribe_AddsMessageToMessages_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Assert
            Assert.True(m_Sut.Messages.Contains(typeof( TestMessage )));
        }

        [Test]
        public void Subscribe_AddsReplacesHandler_ForSameIdTwice()
        {
            // Arrange
            var handlerOther = new TestActionHandlerOther();

            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          handlerOther.Handle);

            // Assert
            Assert.True(m_Sut.Messages.Contains(typeof( TestMessage )));
        }

        [Test]
        public void Subscribers_ReturnsEmpty_ForUnknownMessage()
        {
            // Arrange
            // Act
            IEnumerable <ISubscriberInfo <TestMessage>> actual = m_Sut.Subscribers <TestMessage>();

            // Assert
            Assert.AreEqual(0,
                            actual.Count());
        }

        [Test]
        public void Subscribers_ReturnsHandlers_ForMessage()
        {
            // Arrange
            m_Sut.Subscribe <TestMessage>("Test 1",
                                          m_Handler.Handle);
            m_Sut.Subscribe <TestMessage>("Test 2",
                                          m_Handler.Handle);

            // Act
            IEnumerable <ISubscriberInfo <TestMessage>> actual = m_Sut.Subscribers <TestMessage>().ToArray();

            // Assert
            Assert.AreEqual(2,
                            actual.Count());
        }

        [Test]
        public void Unsubscribe_DoesNothing_ForMessage()
        {
            // Arrange
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Act
            m_Sut.Unsubscribe <TestMessageOther>("SubscriptionId");

            // Assert
            IEnumerable <string> actual = m_Sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.True(actual.Contains("SubscriptionId"));
        }

        [Test]
        public void Unsubscribe_RemovesIdFromIds_WhenCalled()
        {
            // Arrange
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Act
            m_Sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            IEnumerable <string> actual = m_Sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.False(actual.Contains("SubscriptionId"));
        }

        [Test]
        public void Unsubscribe_RemovesMessageFromMessages_ForNoIdsLeft()
        {
            // Arrange
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Act
            m_Sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            Assert.False(m_Sut.Messages.Contains(typeof( TestMessage )));
        }
    }
}