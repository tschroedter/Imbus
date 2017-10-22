using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Imbus.Core.Autofac.NLog.Interfaces;
using JetBrains.Annotations;
using NLog;
using Module = Autofac.Module;

namespace Imbus.Core.Autofac.NLog
{
    public class NLogModule
        : Module
    {
        protected override void AttachToComponentRegistration(
            [NotNull] IComponentRegistry componentRegistry,
            [NotNull] IComponentRegistration registration)
        {
            registration.Preparing += OnComponentPreparing;
            registration.Activated += (sender,
                                       e) => InjectLoggerProperties(e.Instance);
        }

        protected override void Load(
            [NotNull] ContainerBuilder builder)
        {
            builder
                .Register(context => new ImbusLoggerAdapter(LogManager.GetCurrentClassLogger()))
                .As <IImbusLogger>()
                .SingleInstance();
        }

        private static void InjectLoggerProperties(
            [NotNull] object instance)
        {
            Type instanceType = instance.GetType();

            IEnumerable <PropertyInfo> properties =
                instanceType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(p => p.PropertyType == typeof( IImbusLogger ) && p.CanWrite &&
                                        p.GetIndexParameters().Length == 0);

            foreach ( PropertyInfo propToSet in properties )
            {
                propToSet.SetValue(instance,
                                   new ImbusLoggerAdapter(LogManager.GetLogger(instanceType.FullName)),
                                   null);
            }
        }

        private static void OnComponentPreparing(
            [NotNull] object sender,
            [NotNull] PreparingEventArgs e)
        {
            Type t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters
                            .Union(
                                   new[]
                                   {
                                       new ResolvedParameter((p,
                                                              i) => p.ParameterType ==
                                                                    typeof( IImbusLogger ),
                                                             (p,
                                                              i) => new
                                                                 ImbusLoggerAdapter(LogManager
                                                                                        .GetLogger(t.FullName)))
                                   });
        }
    }
}