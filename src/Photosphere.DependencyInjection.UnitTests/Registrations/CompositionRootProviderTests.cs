using System.Collections.Generic;
using System.Linq;
using Moq;
using Photosphere.DependencyInjection.Attributes;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots.Exceptions;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots.ServiceCompositionRoots;
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

        private static IAssembliesProvider GetMockedAssemblyProvider(IAssemblyWrapper assemblyWrapper)
        {
            return new Mock<IAssembliesProvider>().GetInstance(mock =>
            {
                mock.Setup(p => p.Provide()).Returns(() => new[] { assemblyWrapper });
            });
        }

        [Fact]
        internal void Provide_OneAssemblyWithOneCompositionRoot_Finded()
        {
            var assemblyWrapper = new Mock<IAssemblyWrapper>().GetInstance(mock =>
            {
                mock
                    .Setup(p => p.Types)
                    .Returns(() => new[]
                    {
                        typeof(IFoo),
                        typeof(FirstCompositionRoot)
                    });
                mock
                    .Setup(p => p.GetAttributes<CompositionRootAttribute>())
                    .Returns(() => new List<CompositionRootAttribute>());
            });
            var assembliesProvider = GetMockedAssemblyProvider(assemblyWrapper);
            var provider = new CompositionRootProvider(assembliesProvider);

            var result = provider.Provide().Single().GetType();

            Assert.Equal(typeof(FirstCompositionRoot), result);
        }

        [Fact]
        internal void Provide_OneAssemblyWithSeveralCompositionRoots_Exception()
        {
            var assemblyWrapper = new Mock<IAssemblyWrapper>().GetInstance(mock =>
            {
                mock.Setup(p => p.FullName).Returns(() => AssemblyName);
                mock
                    .Setup(p => p.Types)
                    .Returns(() => new[]
                    {
                        typeof(FirstCompositionRoot),
                        typeof(SecondCompositionRoot)
                    });
                mock
                    .Setup(p => p.GetAttributes<CompositionRootAttribute>())
                    .Returns(() => new List<CompositionRootAttribute>());
            });
            var assembliesProvider = GetMockedAssemblyProvider(assemblyWrapper);
            var provider = new CompositionRootProvider(assembliesProvider);

            try
            {
                provider.Provide().IdleEnumerate();
            }
            catch (SeveralCompositionRootsWasFoundException exception)
            {
                Assert.True(
                    exception.Message.Contains(AssemblyName)
                    && exception.Message.Contains(typeof(FirstCompositionRoot).FullName)
                    && exception.Message.Contains(typeof(SecondCompositionRoot).FullName)
                );
            }
        }

        [Fact]
        internal void Provide_OneAssemblyWithOneCompositionRootSpecifiedByAttribute_Finded()
        {
            var compositionRootType = typeof(FirstCompositionRoot);
            var assemblyWrapper = new Mock<IAssemblyWrapper>().GetInstance(mock =>
            {
                mock.Setup(p => p.Types).Returns(() => new[] { typeof(IFoo), compositionRootType });
                mock
                    .Setup(p => p.GetAttributes<CompositionRootAttribute>())
                    .Returns(() => new List<CompositionRootAttribute>
                    {
                        new CompositionRootAttribute(compositionRootType)
                    });
            });
            var assembliesProvider = GetMockedAssemblyProvider(assemblyWrapper);
            var provider = new CompositionRootProvider(assembliesProvider);

            var result = provider.Provide().Single().GetType();

            Assert.Equal(typeof(FirstCompositionRoot), result);
        }

        [Fact]
        internal void Provide_OneAssemblyWithSeveralRegisteredTypes_DefaultCompositionRoot()
        {
            var assemblyWrapper = new Mock<IAssemblyWrapper>().GetInstance(mock =>
            {
                mock
                    .Setup(p => p.GetAttributes<CompositionRootAttribute>())
                    .Returns(() => new List<CompositionRootAttribute>());
                mock
                    .Setup(p => p.GetAttributes<RegisterDependenciesAttribute>())
                    .Returns(() => new List<RegisterDependenciesAttribute>
                    {
                        new RegisterDependenciesAttribute(typeof(IFoo))
                    });
                mock
                    .Setup(p => p.GetAttributes<RegisterDependenciesByAttribute>())
                    .Returns(() => new List<RegisterDependenciesByAttribute>
                    {
                        new RegisterDependenciesByAttribute(typeof(IFoo))
                    });
            });
            var assembliesProvider = GetMockedAssemblyProvider(assemblyWrapper);
            var provider = new CompositionRootProvider(assembliesProvider);

            var result = provider.Provide().Single().GetType();

            Assert.Equal(typeof(DefaultCompositionRoot), result);
        }

        [Fact]
        internal void Provide_OneAssemblyWithoutRegisteredTypes_Null()
        {
            var assemblyWrapper = new Mock<IAssemblyWrapper>().GetInstance(mock =>
            {
                mock
                    .Setup(p => p.GetAttributes<CompositionRootAttribute>())
                    .Returns(() => new List<CompositionRootAttribute>());
                mock
                    .Setup(p => p.GetAttributes<RegisterDependenciesAttribute>())
                    .Returns(() => new List<RegisterDependenciesAttribute>());
                mock
                    .Setup(p => p.GetAttributes<RegisterDependenciesByAttribute>())
                    .Returns(() => new List<RegisterDependenciesByAttribute>());
            });
            var assembliesProvider = GetMockedAssemblyProvider(assemblyWrapper);
            var provider = new CompositionRootProvider(assembliesProvider);

            var result = provider.Provide();

            Assert.Empty(result);
        }
    }
}