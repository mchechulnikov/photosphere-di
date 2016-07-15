using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.LifetimeTests
{
    public class PerContainerLifetimeTests
    {
        private readonly Assembly _targetAssembly = typeof(IPerContainerFoo).Assembly;

        [Fact]
        internal void GetInstance_SameDependenciesInDifferentRequests_Object()
        {
            var container = new DependencyContainer(_targetAssembly);

            var foo1 = container.GetInstance<IPerContainerFoo>();
            var foo2 = container.GetInstance<IPerContainerFoo>();

            Assert.Same(foo1, foo2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesWithInnerDependenciesOnVariousTreeNodes_SameObject()
        {
            var container = new DependencyContainer(_targetAssembly);

            var bar1 = container.GetInstance<IPerContainerBar>();
            var bar2 = container.GetInstance<IPerContainerBar>();

            Assert.Same(bar1, bar2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesOnVariousTreeNodes_SameObject()
        {
            var container = new DependencyContainer(_targetAssembly);

            var serviceWithDependencies = container.GetInstance<IPerContainerDependencies>();
            var foo1 = serviceWithDependencies.PerContainerFoo;
            var foo2 = serviceWithDependencies.PerContainerBar.Foo;

            Assert.Same(foo1, foo2);
        }
    }
}