using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.GetInstaces
{
    public class GetInstanceReadOnlyCollectionResolvingTests
    {
        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_NotNull()
        {
            var container = new DependencyContainer();

            var result = container.GetInstance<IReadOnlyCollection<IService>>();
            
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_NotEmpty()
        {
            var container = new DependencyContainer();

            var result = container.GetInstance<IReadOnlyCollection<IService>>();

            Assert.NotEmpty(result);
        }

        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_ExpectedInstancesTypes()
        {
            var container = new DependencyContainer();

            var result = container.GetInstance<IReadOnlyCollection<IService>>().ToList();

            Assert.Contains(typeof(Service11), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service12), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service21), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service22), result.Select(x => x.GetType()));
        }

        [Fact]
        internal void GetInstance_WithEnumerableDependency_ExpectedInstancesTypes()
        {
            var container = new DependencyContainer();

            var result = container.GetInstance<IReadOnlyCollectionDependencyFoo>().Services.ToList();

            Assert.Contains(typeof(Service11), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service12), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service21), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service22), result.Select(x => x.GetType()));
        }
    }
}