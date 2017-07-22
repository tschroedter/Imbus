using Autofac;
using Imbus.Core.Autofac.NLog.Interfaces;
using JetBrains.Annotations;
using NLog;

namespace Imbus.Core.Autofac.NLog
{
    public class SimpleNLogModule : Module
    {
        protected override void Load(
            [NotNull] ContainerBuilder builder)
        {
            builder
                .Register(context => new ImbusLoggerAdapter(LogManager.GetCurrentClassLogger()))
                .As <IImbusLogger>()
                .SingleInstance();
        }
    }
}