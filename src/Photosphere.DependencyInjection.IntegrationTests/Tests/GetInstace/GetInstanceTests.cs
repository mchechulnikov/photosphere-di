using System.Reflection;
using Photosphere.DependencyInjection.IntegrationTests.Extensions;
using Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.GetInstace
{
    public class GetInstanceTests
    {
        private readonly Assembly _targetAssembly = typeof(IPerRequestFoo).Assembly;

        [Fact]
        internal void GetInstance_ByInterface_NotNull()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IPerRequestFoo>();

            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByInterface_SingleImplementation()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IPerRequestFoo>();

            Assert.IsType<PerRequestFoo>(result);
        }

        [Fact]
        internal void GetInstance_WithDependencies_NotNull()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IPerRequestDependencies>();

            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_WithDependencies_SingleImplementation()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IPerRequestDependencies>();

            Assert.IsType<PerRequestDependencies>(result);
        }

        [Fact]
        internal void GetInstance_WithDependencies_DependenciesNotNull()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = (PerRequestDependencies) container.GetInstance<IPerRequestDependencies>();

            Assert.All(result.GetPrivateReadonlyFieldsObjects(), Assert.NotNull);
        }
    }
}