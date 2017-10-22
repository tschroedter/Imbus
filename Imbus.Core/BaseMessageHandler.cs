using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    [UsedImplicitly]
    public abstract class BaseMessageHandler <T>
        : IMessageHandler <T>
        where T : class
    {
        protected BaseMessageHandler(
            [NotNull] IMessageBus bus,
            [NotNull] string subscriperId)
        {
            Bus = bus;
            SubscriptionId = subscriperId;

            Bus.Subscribe <T>(SubscriptionId,
                              Handle);
        }

        [NotNull]
        [UsedImplicitly]
        protected IMessageBus Bus { get; }

        [NotNull]
        [UsedImplicitly]
        public string SubscriptionId { get; }

        public void Handle(T message)
        {
            HandleMessage(message);
        }

        protected abstract void HandleMessage([UsedImplicitly] T message);
    }
}