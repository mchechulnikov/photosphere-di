using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.Enumerable;
using Photosphere.DependencyInjection.TestAssembly.Enumerable.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.GetInstace
{
    public class GetInstanceEnumerableResolvingTests
    {
        private readonly Assembly _targetAssembly = typeof(EnumerableTestCompositionRoot).Assembly;

        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_NotNull()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IEnumerable<IFoo>>();

            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_NotEmpty()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IEnumerable<IFoo>>();

            Assert.NotEmpty(result);
        }

        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_ExpectedInstancesTypes()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IEnumerable<IFoo>>().ToList();

            Assert.Contains(typeof(Foo1), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo2), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo3), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo4), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo5), result.Select(x => x.GetType()));
        }

        [Fact]
        internal void GetInstance_WithEnumerableDependency_ExpectedInstancesTypes()
        {
            var container = new DependencyContainer(_targetAssembly);

            var result = container.GetInstance<IEnumerableDependencyFoo>().Services.ToList();

            Assert.Contains(typeof(Foo1), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo2), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo3), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo4), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Foo5), result.Select(x => x.GetType()));
        }
    }
}