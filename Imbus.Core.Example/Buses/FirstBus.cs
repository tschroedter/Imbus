using Imbus.Core.Example.Interfaces.Buses;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core.Example.Buses
{
    public class FirstBus
        : InMemoryMessageBus,
          IFirstBus
    {
        public FirstBus(
            [NotNull] ISubscriberStore store,
            [NotNull] IMessageAggregator aggregator)
            : base(store,
                   aggregator)
        {
        }
    }
}