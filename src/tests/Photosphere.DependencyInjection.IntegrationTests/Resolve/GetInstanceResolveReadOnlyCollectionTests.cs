using System.Collections.Generic;
using System.Linq;
using TestAssembly.Enumerable.TestObjects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Resolve
{
    public class GetInstanceResolveReadOnlyCollectionTests : IntegrationTestsBase
    {
        public GetInstanceResolveReadOnlyCollectionTests() : base(typeof(IReadOnlyCollectionDependencyFoo)) {}

        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_NotNull()
        {
            var result = NewContainer.GetInstance<IReadOnlyCollection<IFoo>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_NotEmpty()
        {
            var result = NewContainer.GetInstance<IReadOnlyCollection<IFoo>>();
            Assert.NotEmpty(result);
        }

        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_ExpectedInstancesTypes()
        {
            var result = NewContainer.GetInstance<IReadOnlyCollection<IFoo>>().ToList();

            Assert.Contains(typeof(Foo1), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo2), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo3), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo4), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo5), result.Select(x => x.GetType()));
        }

        [Fact]
        internal void GetInstance_WithEnumerableDependency_ExpectedInstancesTypes()
        {
            var result = NewContainer.GetInstance<IReadOnlyCollectionDependencyFoo>().Services.ToList();

            Assert.Contains(typeof(Foo1), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo2), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo3), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo4), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo5), result.Select(x => x.GetType()));
        }

        [Fact]
        internal void GetInstance_EnumerableByThirdLevelInterface_ExpectedInstancesTypes()
        {
            var result = NewContainer.GetInstance<IReadOnlyCollection<IBar>>().ToList();

            Assert.Contains(typeof(Bar1), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Bar2), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Bar3), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Bar4), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Bar5), result.Select(x => x.GetType()));
        }
    }
}