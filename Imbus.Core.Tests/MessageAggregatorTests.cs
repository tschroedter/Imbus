using System.Threading.Tasks;
using Imbus.Core;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;
using Moq;
using NUnit.Framework;

namespace NCQRS.Core.MessageBus.Tests
{
    [TestFixture]
    internal sealed class MessageAggregatorTests
    {
        [SetUp]
        public void Setup()
        {
            m_Padlock = new object();

            m_Store = new Mock <ISubscriberStore>();
            m_PadlocksStore = new Mock <IPadlockStore>();
            m_PadlocksStore.SetupSequence(m => m.FindOrCreatePadlock(It.IsAny <string>()))
                           .Returns(new object())
                           .Returns(new object());

            m_Sut = new MessageAggregator(m_PadlocksStore.Object);

            m_Sut.IsCallAllHandlersSync = true;

            m_HandlerOne = new TestHandler();
            m_HandlerTwo = new TestHandler();

            m_Handlers = new[]
                         {
                             new SubscriberInfo <TestMessage>("one",
                                                              m_HandlerOne.Handle),
                             new SubscriberInfo <TestMessage>("two",
                                                              m_HandlerTwo.Handle)
                         };
        }

        private object m_Padlock;
        private Mock <ISubscriberStore> m_Store;
        private Mock <IPadlockStore> m_PadlocksStore;
        private MessageAggregator m_Sut;
        private TestHandler m_HandlerOne;
        private TestHandler m_HandlerTwo;
        private SubscriberInfo <TestMessage>[] m_Handlers;

        public class TestHandler
        {
            public bool WasCalled { get; set; }

            public void Handle([NotNull] TestMessage message)
            {
                WasCalled = true;
            }
        }

        public class TestMessage
        {
        }

        [Test]
        public void CreatedTask_CallsHandler_WhenExecuted()
        {
            // Arrange
            Task actual = m_Sut.CreateTask(m_HandlerOne.Handle,
                                           new TestMessage(),
                                           m_Padlock);

            // Act
            actual.Wait();

            // Assert
            Assert.True(m_HandlerOne.WasCalled);
        }

        [Test]
        public void CreateTask_ReturnsTask_WhenCalled()
        {
            // Arrange
            // Act
            Task actual = m_Sut.CreateTask(m_HandlerOne.Handle,
                                           new TestMessage(),
                                           m_Padlock);

            // Assert
            Assert.NotNull(actual);
        }

        [Test]
        public void Publish_Calls_Actions_For_ASync_When_Called()
        {
            // Arrange
            m_Store.Setup(store => store.SubscribersAsync <TestMessage>())
                   .Returns(m_Handlers);

            // Act
            m_Sut.Publish(m_Store.Object,
                          new TestMessage());

            // Assert
            Assert.True(m_HandlerOne.WasCalled,
                        "one.WasCalled");
            Assert.True(m_HandlerTwo.WasCalled,
                        "two.WasCalled");
        }

        [Test]
        public void Publish_Calls_Actions_For_Sync_When_Called()
        {
            // Arrange
            m_Store.Setup(store => store.Subscribers <TestMessage>())
                   .Returns(m_Handlers);

            // Act
            m_Sut.Publish(m_Store.Object,
                          new TestMessage());

            // Assert
            Assert.True(m_HandlerOne.WasCalled,
                        "one.WasCalled");
            Assert.True(m_HandlerTwo.WasCalled,
                        "two.WasCalled");
        }

        [Test]
        public void PublishAsync_Calls_Actions_For_ASync_When_Called()
        {
            // Arrange
            m_Store.Setup(store => store.SubscribersAsync <TestMessage>())
                   .Returns(m_Handlers);

            // Act
            m_Sut.Publish(m_Store.Object,
                          new TestMessage());

            // Assert
            Assert.True(m_HandlerOne.WasCalled,
                        "one.WasCalled");
            Assert.True(m_HandlerTwo.WasCalled,
                        "two.WasCalled");
        }

        [Test]
        public void PublishAsync_Calls_Actions_For_Sync_When_Called()
        {
            // Arrange
            m_Store.Setup(store => store.Subscribers <TestMessage>())
                   .Returns(m_Handlers);

            // Act
            m_Sut.Publish(m_Store.Object,
                          new TestMessage());

            // Assert
            Assert.True(m_HandlerOne.WasCalled,
                        "one.WasCalled");
            Assert.True(m_HandlerTwo.WasCalled,
                        "two.WasCalled");
        }
    }
}