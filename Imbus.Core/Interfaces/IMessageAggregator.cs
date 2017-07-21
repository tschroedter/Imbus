using JetBrains.Annotations;

namespace Imbus.Core.Interfaces
{
    public interface IMessageAggregator
    {
        void Publish <T>([NotNull] ISubscriberStore store,
                         [NotNull] T message);

        void PublishAsync <T>([NotNull] ISubscriberStore store,
                              [NotNull] T message);
    }
}