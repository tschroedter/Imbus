using JetBrains.Annotations;

namespace Imbus.Core.Interfaces
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IMessageHandler <T>
    {
        string SubscriptionId { get; }
        void Handle([NotNull] T message);
    }
}