using Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.LifetimeTests
{
    public class GetInstancePerContainerLifetimeTests : IntegrationTestsBase
    {
        public GetInstancePerContainerLifetimeTests()
            : base(typeof(IPerContainerFoo)) {}

        [Fact]
        internal void GetInstance_SameDependenciesInDifferentRequests_Object()
        {
            var container = NewContainer;

            var foo1 = container.GetInstance<IPerContainerFoo>();
            var foo2 = container.GetInstance<IPerContainerFoo>();

            Assert.Same(foo1, foo2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesWithInnerDependenciesOnVariousTreeNodes_SameObject()
        {
            var container = NewContainer;

            var bar1 = container.GetInstance<IPerContainerBar>();
            var bar2 = container.GetInstance<IPerContainerBar>();

            Assert.Same(bar1, bar2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesOnVariousTreeNodes_SameObject()
        {
            var container = NewContainer;

            var serviceWithDependencies = container.GetInstance<IPerContainerDependencies>();
            var foo1 = serviceWithDependencies.PerContainerFoo;
            var foo2 = serviceWithDependencies.PerContainerBar.Foo;

            Assert.Same(foo1, foo2);
        }
    }
}