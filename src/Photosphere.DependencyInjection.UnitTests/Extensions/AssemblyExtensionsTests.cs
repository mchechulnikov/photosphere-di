using System;
using System.Linq;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.UnitTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.UnitTests.Extensions
{
    public class AssemblyExtensionsTests
    {
        [Theory]
        [InlineData(typeof(IFoo))]
        public void GetAllDerivedTypesOf_Interface_NotNull(Type serviceType)
        {
            var result = serviceType.Assembly.GetAllDerivedTypesOf(serviceType);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(IFoo), typeof(Foo), typeof(Foo1), typeof(Foo2))]
        public void GetAllDerivedTypesOf_Interface_DerivedClass(Type serviceType, Type implType0, Type implType1, Type implType2)
        {
            var result = serviceType.Assembly.GetAllDerivedTypesOf(serviceType).ToList();
            Assert.Contains(implType0, result);
            Assert.Contains(implType1, result);
            Assert.Contains(implType2, result);
        }
    }
}