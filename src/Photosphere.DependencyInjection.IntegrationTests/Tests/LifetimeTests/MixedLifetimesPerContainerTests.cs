using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.LifetimeTests
{
    public class MixedLifetimesPerContainerTests
    {
        [Fact]
        internal void GetInstance_NotNull()
        {
            var container = new DependencyContainer();

            var result = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_AlwaysNewDependenciesFromDifferentRequests_SameObjects()
        {
            var container = new DependencyContainer();

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.AlwaysNewFoo, obj2.AlwaysNewFoo);
        }

        [Fact]
        internal void GetInstance_PerRequestDependenciesFromDifferentRequests_SameObjects()
        {
            var container = new DependencyContainer();

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.PerRequestFoo, obj2.PerRequestFoo);
        }

        [Fact]
        internal void GetInstance_PerContainerDependenciesFromDifferentRequests_SameObjects()
        {
            var container = new DependencyContainer();

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.PerContainerFoo, obj2.PerContainerFoo);
        }
    }
}