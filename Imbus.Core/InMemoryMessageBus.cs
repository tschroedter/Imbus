using System;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    [UsedImplicitly]
    public class InMemoryMessageBus
        : IInMemoryMessageBus
    {
        public InMemoryMessageBus(
            [NotNull] ISubscriberStore store,
            [NotNull] IMessageAggregator aggregator)
        {
            m_Store = store;
            m_Aggregator = aggregator;
        }

        private readonly IMessageAggregator m_Aggregator;
        private readonly object m_Padlock = new object();
        private readonly ISubscriberStore m_Store;

        public void Publish <T>(T message)
            where T : class
        {
            lock ( m_Padlock )
            {
                m_Aggregator.Publish(m_Store,
                                     message);
            }
        }

        public void PublishAsync <T>(T message)
            where T : class
        {
            lock ( m_Padlock )
            {
                m_Aggregator.PublishAsync(m_Store,
                                          message);
            }
        }

        public void Subscribe <T>(string subscriptionId,
                                  Action <T> handler)
            where T : class
        {
            lock ( m_Padlock )
            {
                m_Store.Subscribe(subscriptionId,
                                  handler);
            }
        }

        public void Subscribe <T>(IMessageHandler <T> handler)
            where T : class
        {
            Subscribe <T>(handler.SubscriptionId,
                          handler.Handle);
        }

        public void SubscribeAsync <T>(string subscriptionId,
                                       Action <T> handler)
            where T : class
        {
            lock ( m_Padlock )
            {
                m_Store.SubscribeAsync(subscriptionId,
                                       handler);
            }
        }

        public void SubscribeAsync <T>(IMessageHandler <T> handler)
            where T : class
        {
            SubscribeAsync <T>(handler.SubscriptionId,
                               handler.Handle);
        }
    }
}