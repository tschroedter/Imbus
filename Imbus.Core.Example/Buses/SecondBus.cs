using Imbus.Core.Example.Interfaces.Buses;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core.Example.Buses
{
    public class SecondBus
        : InMemoryMessageBus,
          ISecondBus
    {
        public SecondBus(
            [NotNull] ISubscriberStore store,
            [NotNull] IMessageAggregator aggregator)
            : base(store,
                   aggregator)
        {
        }
    }
}