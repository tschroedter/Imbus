The following types need to registered in an IOC Container:

- InMemoryMessageBus (optional)
- MessageAggregator
- PadlockStore
- SubscriberRepository
- SubscriberStore

The code below shows an example using Autofac (see Imbus.Core.Autofac project):

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