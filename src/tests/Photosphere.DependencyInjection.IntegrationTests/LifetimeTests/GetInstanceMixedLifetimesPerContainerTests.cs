using TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.LifetimeTests
{
    public class GetInstanceMixedLifetimesPerContainerTests : IntegrationTestsBase
    {
        public GetInstanceMixedLifetimesPerContainerTests()
            : base(typeof(IMixedLifetimesPerContainerDependencies)) {}

        [Fact]
        internal void GetInstance_NotNull()
        {
            var result = NewContainer.GetInstance<IMixedLifetimesPerContainerDependencies>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_AlwaysNewDependenciesFromDifferentRequests_SameObjects()
        {
            var container = NewContainer;

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.AlwaysNewFoo, obj2.AlwaysNewFoo);
        }

        [Fact]
        internal void GetInstance_PerRequestDependenciesFromDifferentRequests_SameObjects()
        {
            var container = NewContainer;

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.PerRequestFoo, obj2.PerRequestFoo);
        }

        [Fact]
        internal void GetInstance_PerContainerDependenciesFromDifferentRequests_SameObjects()
        {
            var container = NewContainer;

            var obj1 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerContainerDependencies>();

            Assert.Same(obj1.PerContainerFoo, obj2.PerContainerFoo);
        }
    }
}