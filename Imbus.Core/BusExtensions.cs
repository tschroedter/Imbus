using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

[assembly : InternalsVisibleTo("Sauerteig.Bus.Tests")]

namespace Imbus.Core
{
    [UsedImplicitly]
    public static class BusExtensions
    {
        private const int MaxDegreeOfParallelism = 10;

        // ReSharper disable once MemberCanBePrivate.Global
        internal static readonly Dictionary <string, object> Padlocks = new Dictionary <string, object>();

        private static readonly LimitedConcurrencyLevelTaskScheduler TaskScheduler =
            new LimitedConcurrencyLevelTaskScheduler(MaxDegreeOfParallelism);

        private static readonly TaskFactory Factory = new TaskFactory(TaskScheduler);

        [NotNull]
        internal static Task CreateTask <T>([NotNull] Action <T> handler,
                                            [NotNull] T message,
                                            [NotNull] object padlock)
        {
            Task task = Factory.StartNew(() =>
                                         {
                                             lock ( padlock )
                                             {
                                                 handler(message);
                                             }
                                         },
                                         TaskCreationOptions.None);

            return task;
        }

        internal static object FindOrCreatePadlock(string subscriptionId)
        {
            if ( Padlocks.TryGetValue(subscriptionId,
                                      out object padlock) )
            {
                return padlock;
            }

            padlock = new object();

            Padlocks.Add(subscriptionId,
                         padlock);

            return padlock;
        }
    }
}