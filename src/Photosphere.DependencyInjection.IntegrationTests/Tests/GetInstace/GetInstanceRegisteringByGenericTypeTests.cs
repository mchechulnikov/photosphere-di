using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.Generic.Generic;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.GetInstace
{
    public class GetInstanceRegisteringByGenericTypeTests
    {
        private readonly Assembly _targetAssembly = typeof(IFooForGeneric).Assembly;

        [Fact]
        internal void GetInstance_ByInterfaceForGenericInterface_NotNull()
        {
            var container = new DependencyContainer(_targetAssembly);
            var result = container.GetInstance<IGenericService<IFooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassForGenericInterface_NotNull()
        {
            var container = new DependencyContainer(_targetAssembly);
            var result = container.GetInstance<IGenericService<FooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassForGenericClass_NotNull()
        {
            var container = new DependencyContainer(_targetAssembly);
            var result = container.GetInstance<GenericServiceClass<IFooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByEnumerableForGenericInterface_ExpectedCount()
        {
            var container = new DependencyContainer(_targetAssembly);
            var result = container.GetAllInstances<IGenericService<BarForGeneric>>().Count();
            Assert.Equal(2, result);
        }

        [Fact]
        internal void GetInstance_ByEnumerableForGenericClass_ExpectedCount()
        {
            var container = new DependencyContainer(_targetAssembly);
            var result = container.GetAllInstances<GenericServiceClass<BarForGeneric>>().Count();
            Assert.Equal(2, result);
        }
    }
}