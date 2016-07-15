using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.LifetimeTests
{
    public class MixedLifetimesPerContainerTests
    {
        private readonly Assembly _targetAssembly = typeof(IMixedLifetimesPerContainerDependencies).Assembly;

        [Fact]
        internal void GetInstance_NotNull()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_AlwaysNewDependenciesFromDifferentRequests_SameObjects()
        {
            var container = new DependencyContainer(_targetAssembly);

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.AlwaysNewFoo, obj2.AlwaysNewFoo);
        }

        [Fact]
        internal void GetInstance_PerRequestDependenciesFromDifferentRequests_SameObjects()
        {
            var container = new DependencyContainer(_targetAssembly);

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.PerRequestFoo, obj2.PerRequestFoo);
        }

        [Fact]
        internal void GetInstance_PerContainerDependenciesFromDifferentRequests_SameObjects()
        {
            var container = new DependencyContainer(_targetAssembly);

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.PerContainerFoo, obj2.PerContainerFoo);
        }
    }
}