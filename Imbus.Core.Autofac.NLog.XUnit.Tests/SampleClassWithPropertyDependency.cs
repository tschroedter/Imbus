using System.Diagnostics.CodeAnalysis;
using Imbus.Core.Autofac.NLog.Interfaces;
using JetBrains.Annotations;

namespace Imbus.Core.Autofac.NLog.XUnit.Tests
{
    [ExcludeFromCodeCoverage]
    public class SampleClassWithPropertyDependency
        : ISampleClass
    {
        [UsedImplicitly]
        public IImbusLogger Logger { get; set; }

        public void SampleMessage(
            [NotNull] string message)
        {
            Logger.Debug(message);
        }

        public IImbusLogger GetLogger()
        {
            return Logger;
        }
    }
}