using TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Main
{
    public class GetInstanceTests : IntegrationTestsBase
    {
        public GetInstanceTests() : base(typeof(IPerRequestFoo)) {}

        [Fact]
        internal void GetInstance_ByInterface_NotNull()
        {
            var result = NewContainer.GetInstance<IPerRequestFoo>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByInterface_SingleImplementation()
        {
            var result = NewContainer.GetInstance<IPerRequestFoo>();
            Assert.IsType<PerRequestFoo>(result);
        }

        [Fact]
        internal void GetInstance_WithDependencies_NotNull()
        {
            var result = NewContainer.GetInstance<IPerRequestDependencies>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_WithDependencies_SingleImplementation()
        {
            var result = NewContainer.GetInstance<IPerRequestDependencies>();
            Assert.IsType<PerRequestDependencies>(result);
        }

        [Fact]
        internal void GetInstance_WithDependencies_DependenciesNotNull()
        {
            var result = NewContainer.GetInstance<IPerRequestDependencies>();

            Assert.NotNull(result.Foo);
            Assert.NotNull(result.Bar);
        }
    }
}