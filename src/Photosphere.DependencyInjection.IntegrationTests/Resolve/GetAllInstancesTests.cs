using System;
using System.Linq;
using Photosphere.DependencyInjection.TestAssembly.Enumerable.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Resolve
{
    public class GetAllInstancesTests : IntegrationTestsBase
    {
        public GetAllInstancesTests() : base(typeof(IFoo)) {}

        [Fact]
        internal void GetAllInstances_EnumerableBySecondLevelInterface_NotNull()
        {
            var result = NewContainer.GetAllInstances<IFoo>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetAllInstances_EnumerableBySecondLevelInterface_NotEmpty()
        {
            var result = NewContainer.GetAllInstances<IFoo>();
            Assert.NotEmpty(result);
        }

        [Fact]
        internal void GetAllInstances_EnumerableBySecondLevelInterface_ExpectedInstancesTypes()
        {
            var result = NewContainer.GetAllInstances<IFoo>().ToList();

            Assert.Contains(typeof(Foo1), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo2), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo3), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo4), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo5), result.Select(x => x.GetType()));
        }
    }
}