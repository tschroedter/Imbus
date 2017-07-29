using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Imbus.Core.XUnit.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class PadlockStoreTests
    {
        public PadlockStoreTests()
        {
            m_Sut = new PadlockStore();
        }

        private readonly PadlockStore m_Sut;

        [Fact]
        public void FindOrCreatePadlock_CreatesPadlock_ForNewSubscriptionId()
        {
            // Arrange
            m_Sut.FindOrCreatePadlock("subscriptionId");

            // Act
            object actual = m_Sut.GetPadlock("subscriptionId");

            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
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