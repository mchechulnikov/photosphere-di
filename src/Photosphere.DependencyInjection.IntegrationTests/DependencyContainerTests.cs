using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests
{
    public class DependencyContainerTests
    {
        [Fact]
        internal void GetInstance_ByInterface_NotNull()
        {
            var container = new DependencyContainer();

            container.Initialize();
            var result = container.GetInstance<IFoo>();

            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByInterface_SingleImplementation()
        {
            var container = new DependencyContainer();

            container.Initialize();
            var result = container.GetInstance<IFoo>();

            Assert.IsType<Foo>(result);
        }
    }
}