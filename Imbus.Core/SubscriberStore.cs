using System;
using System.Collections.Generic;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    [UsedImplicitly]
    public class SubscriberStore
        : ISubscriberStore
    {
        public SubscriberStore([NotNull] Func <ISubscriberRepository> factory)
        {
            m_Repository = factory();
            m_AsyncRepository = factory();
        }

        private readonly ISubscriberRepository m_AsyncRepository;
        private readonly ISubscriberRepository m_Repository;

        public void SubscribeAsync <TMessage>(
            string subscriptionId,
            Action <TMessage> handler)
        {
            m_AsyncRepository.Subscribe(subscriptionId,
                                        handler);
        }

        public void Subscribe <TMessage>(
            string subscriptionId,
            Action <TMessage> handler)
        {
            m_Repository.Subscribe(subscriptionId,
                                   handler);
        }

        public void Unsubscribe <TMessage>(string subscriptionId)
        {
            m_Repository.Unsubscribe <TMessage>(subscriptionId);
        }

        public void UnsubscribeAsync <TMessage>(string subscriptionId)
        {
            m_AsyncRepository.Unsubscribe <TMessage>(subscriptionId);
        }

        public IEnumerable <ISubscriberInfo <TMessage>> Subscribers <TMessage>()
        {
            return m_Repository.Subscribers <TMessage>();
        }

        public IEnumerable <ISubscriberInfo <TMessage>> SubscribersAsync <TMessage>()
        {
            return m_AsyncRepository.Subscribers <TMessage>();
        }
    }
}