using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    [UsedImplicitly]
    public abstract class BaseMessageHandlerAsync <T>
        : IMessageHandlerAsync <T>
        where T : class
    {
        protected BaseMessageHandlerAsync(
            [NotNull] IMessageBus bus,
            [NotNull] string subscriperId)
        {
            Bus = bus;
            SubscriptionId = subscriperId;

            Bus.SubscribeAsync <T>(SubscriptionId,
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