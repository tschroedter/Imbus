using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Imbus.Core.Interfaces
{
    public interface ISubscriberRepository
    {
        // ReSharper disable UnusedMember.Global
        IEnumerable <Type> Messages { get; }

        void Subscribe <TMessage>([NotNull] string subscriptionId,
                                  [NotNull] Action <TMessage> handler);

        IEnumerable <ISubscriberInfo <TMessage>> Subscribers <TMessage>();

        void Unsubscribe <TMessage>([NotNull] string subscriptionId);
        // ReSharper restore UnusedMember.Global
    }
}