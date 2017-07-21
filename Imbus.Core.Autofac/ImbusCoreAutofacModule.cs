using System.Diagnostics.CodeAnalysis;
using Autofac;
using Imbus.Core.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core.Autofac
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class ImbusCoreAutofacModule
        : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType <InMemoryMessageBus>()
                   .As <IMessageBus>()
                   .SingleInstance();
            builder.RegisterType <MessageAggregator>()
                   .As <IMessageAggregator>();
            builder.RegisterType <PadlockStore>()
                   .As <IPadlockStore>();
            builder.RegisterType <SubscriberRepository>()
                   .As <ISubscriberRepository>();
            builder.RegisterType <SubscriberStore>()
                   .As <ISubscriberStore>();
        }
    }
}