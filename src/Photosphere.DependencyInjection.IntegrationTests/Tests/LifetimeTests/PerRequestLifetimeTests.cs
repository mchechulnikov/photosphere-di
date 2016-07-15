using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.LifetimeTests
{
    public class PerRequestLifetimeTests
    {
        private readonly Assembly _targetAssembly = typeof(IPerRequestFoo).Assembly;

        [Fact]
        internal void GetInstance_SameDependenciesInDifferentRequests_DifferentObject()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var foo1 = container.GetInstance<IPerRequestFoo>();
                var foo2 = container.GetInstance<IPerRequestFoo>();

                Assert.NotSame(foo1, foo2);
            }
        }

        [Fact]
        internal void GetInstance_SameDependenciesOnVariousTreeNodes_SameObject()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var serviceWithDependencies = container.GetInstance<IPerRequestDependencies>();
                var foo1 = serviceWithDependencies.Foo;
                var foo2 = serviceWithDependencies.Bar.PerRequestFoo;

                Assert.Same(foo1, foo2);
            }
        }
    }
}