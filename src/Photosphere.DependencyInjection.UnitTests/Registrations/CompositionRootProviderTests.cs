using System.Linq;
using Moq;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;
using Photosphere.DependencyInjection.Registrations.Services.Exceptions;
using Photosphere.DependencyInjection.Registrations.ValueObjects;
using Photosphere.DependencyInjection.UnitTests.TestObjects.CompositionRoots;
using Photosphere.DependencyInjection.UnitTests.TestObjects.Objects;
using Photosphere.DependencyInjection.UnitTests.TestUtils.Extensions;
using Xunit;

namespace Photosphere.DependencyInjection.UnitTests.Registrations
{
    public class CompositionRootProviderTests
    {
        private const string AssemblyName = "TestAssembly";

        [Fact]
        internal void Provide_OneAssemblyWithOneCompositionRoot_Finded()
        {
            var assemblyWrapper = new Mock<IAssemblyWrapper>().GetInstance(mock =>
            {
                mock.Setup(p => p.Types).Returns(() => new[] { typeof(IFoo), typeof(FirstCompositionRoot) });
            });
            var assembliesProvider = new Mock<IAssembliesProvider>().GetInstance(mock =>
            {
                mock.Setup(p => p.Provide()).Returns(() => new[] { assemblyWrapper });
            });
            var provider = new CompositionRootProvider(assembliesProvider);

            var result = provider.Provide();

            Assert.Equal(typeof(FirstCompositionRoot), result.Single().GetType());
        }

        [Fact]
        internal void Provide_OneAssemblyWithSeveralCompositionRoots_Exception()
        {
            var assemblyWrapper = new Mock<IAssemblyWrapper>().GetInstance(mock =>
            {
                mock.Setup(p => p.FullName).Returns(() => AssemblyName);
                mock.Setup(p => p.Types).Returns(() => new[] { typeof(FirstCompositionRoot), typeof(SecondCompositionRoot) });
            });
            var assembliesProvider = new Mock<IAssembliesProvider>().GetInstance(mock =>
            {
                mock.Setup(p => p.Provide()).Returns(() => new[] { assemblyWrapper });
            });
            var provider = new CompositionRootProvider(assembliesProvider);

            try
            {
                provider.Provide().IdleEnumerate();
            }
            catch (SeveralCompositionRootsWasFoundException exception)
            {
                Assert.True(
                    exception.Message.Contains(AssemblyName)
                    && exception.Message.Contains(typeof(SecondCompositionRoot).FullName)
                    && exception.Message.Contains(typeof(SecondCompositionRoot).FullName)
                );
            }
        }
    }
}