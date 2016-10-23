using TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.LifetimeTests
{
    public class GetInstancePerRequestLifetimeTests : IntegrationTestsBase
    {
        public GetInstancePerRequestLifetimeTests()
            : base(typeof(IPerRequestFoo)) {}

        [Fact]
        internal void GetInstance_SameDependenciesInDifferentRequests_DifferentObject()
        {
            var container = NewContainer;

            var foo1 = container.GetInstance<IPerRequestFoo>();
            var foo2 = container.GetInstance<IPerRequestFoo>();

            Assert.NotSame(foo1, foo2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesOnVariousTreeNodes_SameObject()
        {
            var container = NewContainer;

            var serviceWithDependencies = container.GetInstance<IPerRequestDependencies>();
            var foo1 = serviceWithDependencies.Foo;
            var foo2 = serviceWithDependencies.Bar.PerRequestFoo;

            Assert.Same(foo1, foo2);
        }
    }
}