using System.Diagnostics.CodeAnalysis;
using Autofac;
using Xunit;

namespace Imbus.Core.Autofac.NLog.XUnit.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class SimpleNLogModuleTests
    {
        public SimpleNLogModuleTests()
        {
            BuildSampleContainer();
        }

        private IContainer m_Container;

        private void BuildSampleContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule <SimpleNLogModule>();

            containerBuilder
                .RegisterType <SampleClassWithConstructorDependency>()
                .Named <ISampleClass>("constructor");

            containerBuilder
                .RegisterType <SampleClassWithPropertyDependency>()
                .Named <ISampleClass>("property")
                .PropertiesAutowired();

            containerBuilder
                .RegisterType <SampleClassToResolveLoggerFromServiceLocator>()
                .Named <ISampleClass>("serviceLocator");

            m_Container = containerBuilder.Build();
        }

        [Fact]
        public void Inject_Logger_To_Constructor_Test()
        {
            var sampleClass = m_Container.ResolveNamed <ISampleClass>("constructor");
            Assert.NotNull(sampleClass.GetLogger());
        }

        [Fact]
        public void Inject_Logger_To_Property_Test()
        {
            var sampleClass = m_Container.ResolveNamed <ISampleClass>("property");

            Assert.NotNull(sampleClass.GetLogger());
        }

        [Fact]
        public void Resolve_Logger_From_LifetimeScope_Test()
        {
            var sampleClass = m_Container.ResolveNamed <ISampleClass>("serviceLocator");

            Assert.NotNull(sampleClass.GetLogger());
        }
    }
}