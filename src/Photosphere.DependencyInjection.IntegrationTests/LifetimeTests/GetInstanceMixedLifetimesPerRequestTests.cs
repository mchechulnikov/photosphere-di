using Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.LifetimeTests
{
    public class GetInstanceMixedLifetimesPerRequestTests : IntegrationTestsBase
    {
        public GetInstanceMixedLifetimesPerRequestTests()
            : base(typeof(IMixedLifetimesPerRequestDependencies)) {}

        [Fact]
        internal void GetInstance_NotNull()
        {
            var result = NewContainer.GetInstance<IMixedLifetimesPerRequestDependencies>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_AlwaysNewDependenciesFromDifferentRequests_NotSame()
        {
            var container = NewContainer;

            var obj1 = container.GetInstance<IMixedLifetimesPerRequestDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerRequestDependencies>();

            Assert.NotSame(obj1.AlwaysNewFoo, obj2.AlwaysNewFoo);
        }

        [Fact]
        internal void GetInstance_PerRequestDependenciesFromDifferentRequests_NotSame()
        {
            var container = NewContainer;

            var obj1 = container.GetInstance<IMixedLifetimesPerRequestDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerRequestDependencies>();

            Assert.NotSame(obj1.PerRequestFoo, obj2.PerRequestFoo);
        }

        [Fact]
        internal void GetInstance_PerContainerDependenciesFromDifferentRequests_SameObjects()
        {
            var container = NewContainer;

            var obj1 = container.GetInstance<IMixedLifetimesPerRequestDependencies>();
            var obj2 = container.GetInstance<IMixedLifetimesPerRequestDependencies>();

            Assert.Same(obj1.PerContainerFoo, obj2.PerContainerFoo);
        }
    }
}