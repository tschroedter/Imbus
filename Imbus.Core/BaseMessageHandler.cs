using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    [UsedImplicitly]
    public abstract class BaseMessageHandler <T> // todo testing
        : IMessageHandler <T>
        where T : class
    {
        protected BaseMessageHandler(
            [NotNull] string subscriperId)
        {
            SubscriptionId = subscriperId;
        }

        [NotNull]
        public string SubscriptionId { get; }

        public void Handle(T message)
        {
            HandleMessage(message);
        }

        protected abstract void HandleMessage(T message);
    }
}