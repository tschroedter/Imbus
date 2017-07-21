using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Imbus.Core.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class BaseMessageHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new TestMessageHandler("Id");
        }

        private class TestMessage
        {
        }

        private class TestMessageHandler
            : BaseMessageHandler <TestMessage>
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

        [Test]
        public void Constructor_Sets_SubscriptionId()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual("Id",
                            m_Sut.SubscriptionId);
        }

        [Test]
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