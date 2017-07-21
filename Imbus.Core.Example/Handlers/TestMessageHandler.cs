using System;
using System.Linq;
using System.Text;
using Imbus.Core.Example.Interfaces;
using Imbus.Core.Example.Messages;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core.Example.Handlers
{
    [UsedImplicitly]
    public class TestMessageHandler
        : BaseMessageHandler <TestMessage>
    {
        public TestMessageHandler(
            [NotNull] IMessageBus bus,
            [NotNull] IImbusLogger logger,
            [NotNull] string subscriperId,
            int expectedNumberOfCalls,
            bool expectedCallsInOrder)
            : base(subscriperId)
        {
            m_Bus = bus;
            m_Logger = logger;
            m_ExpectedNumberOfCalls = expectedNumberOfCalls;
            m_ExpectedCallsInOrder = expectedCallsInOrder;
            m_ReceivedCalls = new bool[expectedNumberOfCalls];
        }

        private int Counter { get; set; }

        private bool IsValid
        {
            get
            {
                return m_ExpectedNumberOfCalls == Counter &&
                       !m_ReceivedCalls.Any(x => x == false);
            }
        }

        [NotNull]
        private readonly IMessageBus m_Bus;

        private readonly bool m_ExpectedCallsInOrder;

        private readonly int m_ExpectedNumberOfCalls;

        [NotNull]
        private readonly IImbusLogger m_Logger;

        private readonly bool[] m_ReceivedCalls;

        protected override void HandleMessage(TestMessage message)
        {
            if ( m_ExpectedCallsInOrder )
            {
                if ( Counter != message.Counter )
                {
                    Console.WriteLine(ReceivedCallsToString());


                    m_Logger.Error($"Handler Counter: {Counter} " +
                                   $"Message Counter: {message.Counter}");
                }
            }

            if ( m_ReceivedCalls [ message.Counter ] )
            {
                throw new Exception($"Received call twice! - {message.Counter}");
            }

            m_ReceivedCalls [ message.Counter ] = true;

            Counter++;

            if ( IsValid )
            {
                string busName = m_Bus.GetType().Name;

                m_Bus.PublishAsync(new FinishedMessage
                                   {
                                       SubscriptionId = SubscriptionId,
                                       BusName = busName
                                   });

                m_Logger.Debug($"[{busName}] {SubscriptionId}: {ReceivedCallsToString()}");
            }
        }

        private string ReceivedCallsToString()
        {
            var builder = new StringBuilder();
            var i = 0;

            foreach ( bool receivedCall in m_ReceivedCalls )
            {
                string value = receivedCall
                                   ? "1"
                                   : "0";
                builder.Append($"[{i}:{value}]");
                i++;
            }

            return builder.ToString();
        }
    }
}