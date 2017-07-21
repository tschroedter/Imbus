using System;
using System.Collections.Generic;
using System.Linq;
using Imbus.Core.Example.Messages;
using JetBrains.Annotations;
using static System.Console;

namespace Imbus.Core.Example.Handlers
{
    [UsedImplicitly]
    public class FinishedMessageHandler
        : BaseMessageHandler <FinishedMessage>
    {
        public FinishedMessageHandler()
            : base("FinishedHandler")
        {
        }

        private readonly Dictionary <string, bool> m_FinishedHandlers = new Dictionary <string, bool>();
        private string m_Id;

        public void Initialize(
            [NotNull] string id,
            [NotNull] TestMessageHandler[] handlers)
        {
            m_Id = id;

            m_FinishedHandlers.Clear();

            foreach ( TestMessageHandler handler in handlers )
            {
                m_FinishedHandlers.Add(handler.SubscriptionId,
                                       false);
            }
        }

        protected override void HandleMessage(FinishedMessage message)
        {
            m_FinishedHandlers [ message.SubscriptionId ] = true;
            WriteLine($"[{message.BusName}] {message.SubscriptionId} has finished!");

            if ( !m_FinishedHandlers.Values.All(x => x) )
            {
                return;
            }

            ForegroundColor = ConsoleColor.Green;
            WriteLine($"[{m_Id}] Handled all messages!");
            ForegroundColor = ConsoleColor.Gray;
        }
    }
}