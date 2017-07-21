using System;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    public class SubscriberInfo <TMessage>
        : ISubscriberInfo <TMessage>
    {
        public SubscriberInfo([NotNull] string subscriptionId,
                              Action <TMessage> handler)
        {
            SubscriptionId = subscriptionId;
            Handler = handler;
        }

        public string SubscriptionId { get; }
        public Action <TMessage> Handler { get; }
    }
}