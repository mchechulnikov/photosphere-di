using System.Collections.Generic;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests
{
    public class EnumerableResolvingTests
    {
        [Fact]
        internal void GetInstance_EnumerableBySecondLevelInterface_NotNull()
        {
            var container = new DependencyContainer();

            var result = container.GetInstance<IEnumerable<IService>>();

            Assert.NotNull(result);
        }
    }
}