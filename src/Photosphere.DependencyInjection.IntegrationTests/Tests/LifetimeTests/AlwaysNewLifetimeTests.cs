using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.LifetimeTests
{
    public class AlwaysNewLifetimeTests
    {
        private readonly Assembly _targetAssembly = typeof(IAlwaysNewFoo).Assembly;

        [Fact]
        internal void GetInstance_SameDependenciesInDifferentRequests_DifferentObject()
        {
            var container = new DependencyContainer(_targetAssembly);

            var foo1 = container.GetInstance<IAlwaysNewFoo>();
            var foo2 = container.GetInstance<IAlwaysNewFoo>();

            Assert.NotSame(foo1, foo2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesOnVariousTreeNodes_DifferentObject()
        {
            var container = new DependencyContainer(_targetAssembly);

            var serviceWithDependencies = container.GetInstance<IAlwaysNewDependencies>();
            var foo1 = serviceWithDependencies.Foo;
            var foo2 = serviceWithDependencies.Bar.AlwaysNewFoo;

            Assert.NotSame(foo1, foo2);
        }
    }
}