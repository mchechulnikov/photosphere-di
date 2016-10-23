using TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.LifetimeTests
{
    public class GetInstanceAlwaysNewLifetimeTests : IntegrationTestsBase
    {
        public GetInstanceAlwaysNewLifetimeTests() : base(typeof(IAlwaysNewFoo)) {}

        [Fact]
        internal void GetInstance_SameDependenciesInDifferentRequests_DifferentObject()
        {
            var container = NewContainer;

            var foo1 = container.GetInstance<IAlwaysNewFoo>();
            var foo2 = container.GetInstance<IAlwaysNewFoo>();

            Assert.NotSame(foo1, foo2);
        }

        [Fact]
        internal void GetInstance_SameDependenciesOnVariousTreeNodes_DifferentObject()
        {
            var container = NewContainer;

            var serviceWithDependencies = container.GetInstance<IAlwaysNewDependencies>();
            var foo1 = serviceWithDependencies.Foo;
            var foo2 = serviceWithDependencies.Bar.AlwaysNewFoo;

            Assert.NotSame(foo1, foo2);
        }
    }
}