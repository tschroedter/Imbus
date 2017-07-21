using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Imbus.Core.Interfaces
{
    public interface ISubscriberStore
    {
        // ReSharper disable UnusedMember.Global
        void Subscribe <TMessage>(
            [NotNull] string subscriptionId,
            [NotNull] Action <TMessage> handler);

        void SubscribeAsync <TMessage>(
            [NotNull] string subscriptionId,
            [NotNull] Action <TMessage> handler);

        IEnumerable <ISubscriberInfo <TMessage>> Subscribers <TMessage>();
        IEnumerable <ISubscriberInfo <TMessage>> SubscribersAsync <TMessage>();

        void Unsubscribe <TMessage>(
            [NotNull] string subscriptionId);

        void UnsubscribeAsync <TMessage>(
            [NotNull] string subscriptionId);
        // ReSharper restore UnusedMember.Global
    }
}