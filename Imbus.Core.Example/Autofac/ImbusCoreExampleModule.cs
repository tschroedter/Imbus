using System.Diagnostics.CodeAnalysis;
using Autofac;
using Imbus.Core.Example.Buses;
using Imbus.Core.Example.Handlers;
using Imbus.Core.Example.Messages;
using JetBrains.Annotations;

namespace Imbus.Core.Example.Autofac
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class ImbusCoreExampleModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType <FirstBus>()
                   .AsImplementedInterfaces()
                   .SingleInstance();
            builder.RegisterType <SecondBus>()
                   .AsImplementedInterfaces()
                   .SingleInstance();
            builder.RegisterType <TestMessage>();
            builder.RegisterType <FinishedMessage>();
            builder.RegisterType <TestMessageHandler>();
            builder.RegisterType <FinishedMessageHandler>();
            builder.RegisterType <MessageBusTester>();
        }
    }
}