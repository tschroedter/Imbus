using System;
using System.Collections.Generic;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace Imbus.Core.Interfaces
{
    public interface ISubscriberRepository
    {
        IEnumerable <Type> Messages { get; }

        void Subscribe <TMessage>([NotNull] string subscriptionId,
                                  [NotNull] Action <TMessage> handler);

        IEnumerable <SubscriberInfo <TMessage>> Subscribers <TMessage>();

        void Unsubscribe <TMessage>([NotNull] string subscriptionId);
    }
}