using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests
{
    public class DependencyContainerTests
    {
        [Fact]
        internal void GetInstance_ValidCompositionRoot_ValidResult()
        {
            var container = new DependencyContainer();

            container.Initialize();
            var result = container.GetInstance<IFoo>();

            Assert.NotNull(result);
        }
    }
}