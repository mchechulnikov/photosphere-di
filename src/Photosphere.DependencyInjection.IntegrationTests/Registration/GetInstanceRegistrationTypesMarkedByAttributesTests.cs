using Photosphere.DependencyInjection.TestAssembly.RegisterByAttribute.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Registration
{
    public class GetInstanceRegistrationTypesMarkedByAttributesTests : IntegrationTestsBase
    {
        public GetInstanceRegistrationTypesMarkedByAttributesTests() : base(typeof(IFooForAttribute)) {}

        [Fact]
        internal void GetInstance_ByInterfaceTypeMarkedInterface_NotNull()
        {
            var result = NewContainer.GetInstance<IFooForAttribute>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassTypeMarkedInterface_NotNull()
        {
            var result = NewContainer.GetInstance<FooForAttribute>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByInterfaceTypeMarkedClass_NotNull()
        {
            var result = NewContainer.GetInstance<BarForAttribute>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_EnumerableByClassTypeMarkedClass_NotEmptyCollection()
        {
            var result = NewContainer.GetAllInstances<BarForAttribute>();
            Assert.NotEmpty(result);
        }

        [Fact]
        internal void GetInstance_ByInterfaceTypeMarkedClass_SameType()
        {
            var result = NewContainer.GetInstance<BarForAttribute>().GetType();
            Assert.Same(typeof(BarForAttribute), result);
        }
    }
}