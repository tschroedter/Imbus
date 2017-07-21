using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    public class MessageAggregator
        : IMessageAggregator
    {
        public MessageAggregator(
            [NotNull] IPadlockStore padlockStore)
        {
            m_PadlockStore = padlockStore;

            var taskScheduler = new LimitedConcurrencyLevelTaskScheduler(MaxDegreeOfParallelism);
            m_Factory = new TaskFactory(taskScheduler);
        }

        private const int MaxDegreeOfParallelism = 10;

        [UsedImplicitly]
        public bool IsCallAllHandlersSync { get; set; }

        private readonly TaskFactory m_Factory;
        private readonly IPadlockStore m_PadlockStore;

        public void Publish <T>(ISubscriberStore store,
                                T message)
        {
            CallHandlers(message,
                         store.Subscribers <T>());

            CallHandlersAsync(message,
                              store.SubscribersAsync <T>());
        }

        public void PublishAsync <T>(ISubscriberStore store,
                                     T message)
        {
            CallHandlersAsync(message,
                              store.Subscribers <T>());

            CallHandlersAsync(message,
                              store.SubscribersAsync <T>());
        }

        [NotNull]
        [UsedImplicitly]
        public Task CreateTask <T>([NotNull] Action <T> handler,
                                   [NotNull] T message,
                                   [NotNull] object padlock)
        {
            Task task = m_Factory.StartNew(() =>
                                           {
                                               lock ( padlock )
                                               {
                                                   handler(message);
                                               }
                                           },
                                           TaskCreationOptions.None);

            return task;
        }

        private void CallHandlers <T>(T message,
                                      [NotNull] IEnumerable <ISubscriberInfo <T>> information)
        {
            foreach ( ISubscriberInfo <T> info in information )
            {
                object padlock = m_PadlockStore.FindOrCreatePadlock(info.SubscriptionId);

                lock ( padlock )
                {
                    info.Handler(message);
                }
            }
        }

        private void CallHandlersAsync <T>([NotNull] T message,
                                           [NotNull] IEnumerable <ISubscriberInfo <T>> information)
        {
            foreach ( ISubscriberInfo <T> info in information )
            {
                object padlock = m_PadlockStore.FindOrCreatePadlock(info.SubscriptionId);

                Task task = CreateTask(info.Handler,
                                       message,
                                       padlock);

                if ( IsCallAllHandlersSync )
                {
                    task.Wait(5000);
                }
            }
        }
    }
}