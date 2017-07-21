using System;

namespace Imbus.Core.Interfaces
{
    public interface ISubscriberInfo <in T>
    {
        string SubscriptionId { get; }
        Action <T> Handler { get; }
    }
}