using System.Reflection;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects.ByAttributes;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.GetInstace
{
    public class GetInstanceRegisteringTypesMarkedByAttributesTests
    {
        private readonly Assembly _targetAssembly = typeof(IFooForAttribute).Assembly;

        [Fact]
        internal void GetInstance_ByInterfaceTypeMarkedInterface_NotNull()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var result = container.GetInstance<IFooForAttribute>();
                Assert.NotNull(result);
            }
        }

        [Fact]
        internal void GetInstance_ByClassTypeMarkedInterface_NotNull()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var result = container.GetInstance<FooForAttribute>();
                Assert.NotNull(result);
            }
        }

        [Fact]
        internal void GetInstance_ByInterfaceTypeMarkedClass_NotNull()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var result = container.GetInstance<BarForAttribute>();
                Assert.NotNull(result);
            }
        }

        [Fact]
        internal void GetInstance_EnumerableByClassTypeMarkedClass_NotEmptyCollection()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var result = container.GetAllInstances<BarForAttribute>();
                Assert.NotEmpty(result);
            }
        }

        [Fact]
        internal void GetInstance_ByInterfaceTypeMarkedClass_SameType()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var result = container.GetInstance<BarForAttribute>().GetType();
                Assert.Same(typeof(BarForAttribute), result);
            }
        }
    }
}