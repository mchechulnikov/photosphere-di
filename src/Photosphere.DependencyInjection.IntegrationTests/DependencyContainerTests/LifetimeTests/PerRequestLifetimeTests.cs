using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.DependencyContainerTests.LifetimeTests
{
    public class PerRequestLifetimeTests
    {
        [Fact]
        internal void GetInstance_SameDependenciesInDifferentRequests_DifferentObject()
        {
            var container = new DependencyContainer();

            var foo1 = container.GetInstance<IPerRequestFoo>();
            var foo2 = container.GetInstance<IPerRequestFoo>();

            Assert.NotSame(foo1, foo2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesOnVariousTreeNodes_SameObject()
        {
            var container = new DependencyContainer();

            var serviceWithDependencies = container.GetInstance<IWithPerRequestDependencies>();
            var foo1 = serviceWithDependencies.Foo;
            var foo2 = serviceWithDependencies.Bar.PerRequestFoo;

            Assert.Same(foo1, foo2);
        }
    }
}