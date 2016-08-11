using System;
using System.Linq;
using Photosphere.DependencyInjection.Types;
using Photosphere.DependencyInjection.UnitTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.UnitTests.Types
{
    public class TypesProviderTests
    {
        [Theory]
        [InlineData(typeof(IFoo))]
        public void GetAllDerivedTypes_Interface_NotNull(Type serviceType)
        {
            var typesProvider = new TypesProvider();
            var result = typesProvider.GetAllDerivedTypesFrom(serviceType, serviceType.Assembly);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(IFoo), typeof(Foo), typeof(Foo1), typeof(Foo2))]
        public void GetAllDerivedTypes_Interface_DerivedClass(Type serviceType, Type implType0, Type implType1, Type implType2)
        {
            var typesProvider = new TypesProvider();
            var result = typesProvider.GetAllDerivedTypesFrom(serviceType, serviceType.Assembly).ToList();
            Assert.Contains(implType0, result);
            Assert.Contains(implType1, result);
            Assert.Contains(implType2, result);
        }
    }
}