using System;
using JetBrains.Annotations;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global
namespace Imbus.Core.Interfaces
{
    public interface IMessageBus
    {
        void Publish <T>(T message)
            where T : class;

        void PublishAsync <T>(T message)
            where T : class;

        void Subscribe <T>(
            [NotNull] string subscriptionId,
            [NotNull] Action <T> handler)
            where T : class;

        void Subscribe <T>(
            [NotNull] IMessageHandler <T> handler)
            where T : class;

        void SubscribeAsync <T>(
            [NotNull] string subscriptionId,
            [NotNull] Action <T> handler)
            where T : class;

        void SubscribeAsync <T>(
            [NotNull] IMessageHandler <T> handler)
            where T : class;
    }
}