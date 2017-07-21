using JetBrains.Annotations;

namespace Imbus.Core.Interfaces
{
    public interface IPadlockStore
    {
        // ReSharper disable UnusedMember.Global
        [CanBeNull]
        object GetPadlock([NotNull] string subscriptionId);

        [NotNull]
        object FindOrCreatePadlock(string subscriptionId);
        // ReSharper restore UnusedMember.Global
    }
}