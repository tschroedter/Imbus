using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Imbus.Core.Interfaces;
using Xunit;

namespace Imbus.Core.XUnit.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class SubscriberRepositoryTests
    {
        public SubscriberRepositoryTests()
        {
            m_Handler = new TestActionHandler();

            m_Sut = new SubscriberRepository();
        }

        private readonly TestActionHandler m_Handler;
        private readonly SubscriberRepository m_Sut;

        [Fact]
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

            Assert.True(Contains(actual,
                                 "SubscriptionId 1"));
            Assert.True(Contains(actual,
                                 "SubscriptionId 2"));
        }

        [Fact]
        public void Subscribe_AddsIdToIds_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Assert
            IEnumerable <string> actual = m_Sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.True(Contains(actual,
                                 "SubscriptionId"));
        }

        [Fact]
        public void Subscribe_AddsMessageToMessages_ForSameMessageRegistraion()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("Test 1",
                                          m_Handler.Handle);
            m_Sut.Subscribe <TestMessage>("Test 2",
                                          m_Handler.Handle);

            // Assert
            Assert.NotNull(m_Sut.Messages.FirstOrDefault(x => x == typeof( TestMessage )));
        }

        [Fact]
        public void Subscribe_AddsMessageToMessages_WhenCalled()
        {
            // Arrange
            // Act
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Assert
            Assert.NotNull(m_Sut.Messages.FirstOrDefault(x => x == typeof( TestMessage )));
        }

        [Fact]
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
            Assert.NotNull(m_Sut.Messages.FirstOrDefault(x => x == typeof( TestMessage )));
        }

        [Fact]
        public void Subscribers_ReturnsEmpty_ForUnknownMessage()
        {
            // Arrange
            // Act
            IEnumerable <ISubscriberInfo <TestMessage>> actual = m_Sut.Subscribers <TestMessage>();

            // Assert
            Assert.True(0 == actual.Count());
        }

        [Fact]
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
            Assert.Equal(2,
                         actual.Count());
        }

        [Fact]
        public void Unsubscribe_DoesNothing_ForMessage()
        {
            // Arrange
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Act
            m_Sut.Unsubscribe <TestMessageOther>("SubscriptionId");

            // Assert
            IEnumerable <string> actual = m_Sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.True(Contains(actual,
                                 "SubscriptionId"));
        }

        [Fact]
        public void Unsubscribe_RemovesIdFromIds_WhenCalled()
        {
            // Arrange
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Act
            m_Sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            IEnumerable <string> actual = m_Sut.GetSubscriptionIdsForMessage <TestMessage>();

            Assert.False(Contains(actual,
                                  "SubscriptionId"));
        }

        [Fact]
        public void Unsubscribe_RemovesMessageFromMessages_ForNoIdsLeft()
        {
            // Arrange
            m_Sut.Subscribe <TestMessage>("SubscriptionId",
                                          m_Handler.Handle);

            // Act
            m_Sut.Unsubscribe <TestMessage>("SubscriptionId");

            // Assert
            Assert.Null(m_Sut.Messages.FirstOrDefault(x => x == typeof( TestMessage )));
        }

        private static bool Contains(IEnumerable <string> actual,
                                     string text)
        {
            return actual.FirstOrDefault(x => x.CompareTo(text) == 0) != null;
        }

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
    }
}