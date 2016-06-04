using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.DependencyContainerTests
{
    public class GetInstanceTests
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

        [Fact]
        internal void GetInstance_WithDependencies_NotNull()
        {
            var container = new DependencyContainer();

            container.Initialize();
            var result = container.GetInstance<ITestServiceWithDependencies>();

            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_WithDependencies_SingleImplementation()
        {
            var container = new DependencyContainer();

            container.Initialize();
            var result = container.GetInstance<ITestServiceWithDependencies>();

            Assert.IsType<TestServiceWithDependencies>(result);
        }

        [Fact]
        internal void GetInstance_WithDependencies_DependenciesNotNull()
        {
            var container = new DependencyContainer();

            container.Initialize();
            var instance = (TestServiceWithDependencies) container.GetInstance<ITestServiceWithDependencies>();
            var dependencies = instance.GetPrivateReadonlyFieldsObjectsOf();

            Assert.All(dependencies, Assert.NotNull);
        }
    }
}