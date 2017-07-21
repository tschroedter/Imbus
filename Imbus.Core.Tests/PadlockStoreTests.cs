using Imbus.Core;
using NUnit.Framework;

namespace NCQRS.Core.MessageBus.Tests
{
    [TestFixture]
    internal sealed class PadlockStoreTests
    {
        [SetUp]
        public void Setup()
        {
            m_Sut = new PadlockStore();
        }

        private PadlockStore m_Sut;

        [Test]
        public void FindOrCreatePadlock_CreatesPadlock_ForNewSubscriptionId()
        {
            // Arrange
            m_Sut.FindOrCreatePadlock("subscriptionId");

            // Act
            object actual = m_Sut.GetPadlock("subscriptionId");

            // Assert
            Assert.NotNull(actual);
        }

        [Test]
        public void FindOrCreatePadlock_ReturnsExistingPadlock_ForKnownSubscriptionId()
        {
            // Arrange
            m_Sut.FindOrCreatePadlock("subscriptionId");
            object expected = m_Sut.GetPadlock("subscriptionId");

            m_Sut.FindOrCreatePadlock("subscriptionId");

            // Act
            object actual = m_Sut.GetPadlock("subscriptionId");

            // Assert
            Assert.True(expected == actual);
        }
    }
}