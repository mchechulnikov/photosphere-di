using System.Linq;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests
{
    public class GetAllInstancesTests
    {
        [Fact]
        internal void GetAllInstances_EnumerableBySecondLevelInterface_NotNull()
        {
            var container = new DependencyContainer();

            var result = container.GetAllInstances<IService>();

            Assert.NotNull(result);
        }

        [Fact]
        internal void GetAllInstances_EnumerableBySecondLevelInterface_NotEmpty()
        {
            var container = new DependencyContainer();

            var result = container.GetAllInstances<IService>();

            Assert.NotEmpty(result);
        }

        [Fact]
        internal void GetAllInstances_EnumerableBySecondLevelInterface_ExpectedInstancesTypes()
        {
            var container = new DependencyContainer();

            var result = container.GetAllInstances<IService>().ToList();

            Assert.Contains(typeof(Service11), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service12), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service21), result.Select(x => x.GetType()));
            Assert.Contains(typeof(Service22), result.Select(x => x.GetType()));
        }
    }
}