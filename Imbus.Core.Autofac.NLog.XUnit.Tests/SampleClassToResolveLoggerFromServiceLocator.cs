using System.Diagnostics.CodeAnalysis;
using Autofac;
using Imbus.Core.Autofac.NLog.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core.Autofac.NLog.XUnit.Tests
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class SampleClassToResolveLoggerFromServiceLocator
        : ISampleClass
    {
        public SampleClassToResolveLoggerFromServiceLocator(
            [NotNull] ILifetimeScope serviceLocator)
        {
            m_Logger = serviceLocator.Resolve <IImbusLogger>();
        }

        private readonly IImbusLogger m_Logger;

        public void SampleMessage(string message)
        {
            m_Logger.Debug(message);
        }

        public IImbusLogger GetLogger()
        {
            return m_Logger;
        }
    }
}