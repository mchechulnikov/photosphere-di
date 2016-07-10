using System.Collections;
using System.Linq;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects.Generic;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.GetInstaces
{
    public class GetInstanceRegisteringByGenericTypeTests
    {
        [Fact]
        internal void GetInstance_ByInterfaceForGenericInterface_NotNull()
        {
            var container = new DependencyContainer();
            var result = container.GetInstance<IGenericService<IFoo>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassForGenericInterface_NotNull()
        {
            var container = new DependencyContainer();
            var result = container.GetInstance<IGenericService<Foo>>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByClassForGenericClass_NotNull()
        {
            var container = new DependencyContainer();
            var result = container.GetInstance<GenericServiceClass<IFoo>>();
            Assert.NotNull(result);
        }

        //[Fact]
        //internal void GetInstance_ByEnumerableForGenericClass_ExpectedCount()
        //{
        //    var container = new DependencyContainer();

        //    var enumerable = container.GetInstance(typeof(GenericServiceClass<>));
        //    var result = ((ICollection) enumerable).Count;

        //    Assert.Equal(3, result);
        //}
    }
}