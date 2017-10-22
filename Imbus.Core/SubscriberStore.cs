using System;
using System.Collections.Generic;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    [UsedImplicitly]
    public class SubscriberStore : ISubscriberStore
    {
        public SubscriberStore([NotNull] ISubscriberRepository syncSyncRepository,
                               [NotNull] ISubscriberRepository asyncRepository)
        {
            m_SyncRepository = syncSyncRepository;
            m_AsyncRepository = asyncRepository;
        }

        private readonly ISubscriberRepository m_AsyncRepository;
        private readonly ISubscriberRepository m_SyncRepository;

        public void SubscribeAsync <TMessage>(string subscriptionId,
                                              Action <TMessage> handler)
        {
            m_AsyncRepository.Subscribe(subscriptionId,
                                        handler);
        }

        public void Subscribe <TMessage>(string subscriptionId,
                                         Action <TMessage> handler)
        {
            m_SyncRepository.Subscribe(subscriptionId,
                                       handler);
        }

        public void Unsubscribe <TMessage>(string subscriptionId)
        {
            m_SyncRepository.Unsubscribe <TMessage>(subscriptionId);
        }

        public void UnsubscribeAsync <TMessage>(string subscriptionId)
        {
            m_AsyncRepository.Unsubscribe <TMessage>(subscriptionId);
        }

        public IEnumerable <SubscriberInfo <TMessage>> Subscribers <TMessage>()
        {
            return m_SyncRepository.Subscribers <TMessage>();
        }

        public IEnumerable <SubscriberInfo <TMessage>> SubscribersAsync <TMessage>()
        {
            return m_AsyncRepository.Subscribers <TMessage>();
        }
    }
}