using System.Collections.Generic;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core
{
    [UsedImplicitly]
    public class PadlockStore
        : IPadlockStore
    {
        private readonly Dictionary <string, object> m_Padlocks = new Dictionary <string, object>();

        public object FindOrCreatePadlock(string subscriptionId)
        {
            object padlock;

            if ( m_Padlocks.TryGetValue(subscriptionId,
                                        out padlock) )
            {
                return padlock;
            }

            padlock = new object();

            m_Padlocks.Add(subscriptionId,
                           padlock);

            return padlock;
        }

        public object GetPadlock(string subscriptionId)
        {
            object padlock;

            return m_Padlocks.TryGetValue(subscriptionId,
                                          out padlock)
                       ? padlock
                       : null;
        }
    }
}