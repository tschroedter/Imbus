using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Imbus.Core.Example.Handlers;
using Imbus.Core.Example.Messages;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;
using static System.Console;

namespace Imbus.Core.Example
{
    public class MessageBusTester
    {
        public MessageBusTester(
            [NotNull] IContainer container,
            [NotNull] IMessageBus bus,
            [NotNull] string id)
        {
            m_Container = container;
            m_Bus = bus;
            m_Id = id;

            NumberOfHandlers = 1;
            NumberOfMessages = 1;
        }

        public int NumberOfHandlers { private get; set; }
        public int NumberOfMessages { private get; set; }

        [NotNull]
        private readonly IMessageBus m_Bus;

        [NotNull]
        private readonly IContainer m_Container;

        private readonly string m_Id;

        private FinishedMessageHandler m_FinishedHandler;

        [UsedImplicitly]
        private TestMessageHandler[] m_Handlers;

        public void Initialize()
        {
            m_Handlers = CreateHandlers();

            m_FinishedHandler = m_Container.Resolve <FinishedMessageHandler>();
            m_FinishedHandler.Initialize(m_Id,
                                         m_Handlers);

            m_Bus.Subscribe(m_FinishedHandler);
        }

        public void Run()
        {
            var task = new Task(SendMessages);
            task.Start();
        }

        private TestMessageHandler[] CreateHandlers()
        {
            var handlers = new TestMessageHandler[NumberOfHandlers];

            for ( var i = 0 ; i < NumberOfHandlers ; i++ )
            {
                var zero = new NamedParameter("bus",
                                              m_Bus);
                var one = new NamedParameter("subscriperId",
                                             i.ToString());
                var two = new NamedParameter("expectedNumberOfCalls",
                                             NumberOfMessages);
                var three = new NamedParameter("expectedCallsInOrder",
                                               true);
                Parameter[] parameters =
                {
                    zero,
                    one,
                    two,
                    three
                };

                var handler = m_Container.Resolve <TestMessageHandler>(parameters);
                m_Bus.Subscribe(handler);

                handlers [ i ] = handler;
            }

            return handlers;
        }

        private void SendMessages()
        {
            for ( var i = 0 ; i < NumberOfMessages ; i++ )
            {
                m_Bus.Publish(new TestMessage
                              {
                                  Counter = i
                              });
            }

            string name = m_Bus.GetType().Name;

            WriteLine($"[{name}] Finished sending messages!");
        }
    }
}