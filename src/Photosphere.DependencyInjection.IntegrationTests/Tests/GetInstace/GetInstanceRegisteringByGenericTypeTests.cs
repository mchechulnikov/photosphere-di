using System.Linq;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects.Generic;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.GetInstace
{
    public class GetInstanceRegisteringByGenericTypeTests
    {
        [Fact]
        internal void GetInstance_ByInterfaceForGenericInterface_NotNull()
        {
            var container = new DependencyContainer();
            var result = container.GetInstance<IGenericService<IFooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassForGenericInterface_NotNull()
        {
            var container = new DependencyContainer();
            var result = container.GetInstance<IGenericService<FooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassForGenericClass_NotNull()
        {
            var container = new DependencyContainer();
            var result = container.GetInstance<GenericServiceClass<IFooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByEnumerableForGenericInterface_ExpectedCount()
        {
            var container = new DependencyContainer();
            var result = container.GetAllInstances<IGenericService<BarForGeneric>>().Count();
            Assert.Equal(2, result);
        }

        [Fact]
        internal void GetInstance_ByEnumerableForGenericClass_ExpectedCount()
        {
            var container = new DependencyContainer();
            var result = container.GetAllInstances<GenericServiceClass<BarForGeneric>>().Count();
            Assert.Equal(2, result);
        }
    }
}