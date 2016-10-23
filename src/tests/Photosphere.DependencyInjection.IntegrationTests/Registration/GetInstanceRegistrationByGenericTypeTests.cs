using System.Linq;
using TestAssembly.Generic.Generic;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Registration
{
    public class GetInstanceRegistrationByGenericTypeTests : IntegrationTestsBase
    {
        public GetInstanceRegistrationByGenericTypeTests() : base(typeof(IFooForGeneric)) {}

        [Fact]
        internal void GetInstance_ByInterfaceForGenericInterface_NotNull()
        {
            var result = NewContainer.GetInstance<IGenericService<IFooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassForGenericInterface_NotNull()
        {
            var result = NewContainer.GetInstance<IGenericService<FooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassForGenericClass_NotNull()
        {
            var result = NewContainer.GetInstance<GenericServiceClass<IFooForGeneric>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByEnumerableForGenericInterface_ExpectedCount()
        {
            var result = NewContainer.GetAllInstances<IGenericService<BarForGeneric>>().Count();
            Assert.Equal(2, result);
        }

        [Fact]
        internal void GetInstance_ByEnumerableForGenericClass_ExpectedCount()
        {
            var result = NewContainer.GetAllInstances<GenericServiceClass<BarForGeneric>>().Count();
            Assert.Equal(2, result);
        }
    }
}