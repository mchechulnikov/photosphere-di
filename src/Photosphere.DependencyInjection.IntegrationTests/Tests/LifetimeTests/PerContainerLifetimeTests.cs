using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.LifetimeTests
{
    public class PerContainerLifetimeTests
    {
        [Fact]
        internal void GetInstance_SameDependenciesInDifferentRequests_Object()
        {
            var container = new DependencyContainer();

            var foo1 = container.GetInstance<IPerContainerFoo>();
            var foo2 = container.GetInstance<IPerContainerFoo>();

            Assert.Same(foo1, foo2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesOnVariousTreeNodes_SameObject()
        {
            var container = new DependencyContainer();

            var serviceWithDependencies = container.GetInstance<IPerContainerDependencies>();
            var foo1 = serviceWithDependencies.PerContainerFoo;
            var foo2 = serviceWithDependencies.PerContainerBar.Foo;

            Assert.Same(foo1, foo2);
        }
    }
}