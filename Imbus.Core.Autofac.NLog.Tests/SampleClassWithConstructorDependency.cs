using System.Diagnostics.CodeAnalysis;
using Imbus.Core.Autofac.NLog.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core.Autofac.NLog.Tests
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    public class SampleClassWithConstructorDependency
        : ISampleClass
    {
        public SampleClassWithConstructorDependency(
            [NotNull] IImbusLogger logger)
        {
            m_Logger = logger;
        }

        private readonly IImbusLogger m_Logger;

        public void SampleMessage(
            [NotNull] string message)
        {
            m_Logger.Debug(message);
        }

        public IImbusLogger GetLogger()
        {
            return m_Logger;
        }
    }
}