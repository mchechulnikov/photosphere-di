using System;
using Photosphere.DependencyInjection.Extensions;
using Xunit;
using Photosphere.DependencyInjection.UnitTests.Utils;

namespace Photosphere.DependencyInjection.UnitTests.Extensions
{
    public class AssemblyExtensionsTests
    {
        [Theory]
        [InlineData(typeof(IFoo))]
        public void GetFirstImplementationTypeOf_Interface_NotNull(Type serviceType)
        {
            var result = serviceType.Assembly.GetFirstOrDefaultImplementationTypeOf(serviceType);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(IFoo), typeof(Foo))]
        public void GetFirstImplementationTypeOf_Interface_DerivedClass(Type serviceType, Type implementationType)
        {
            var result = serviceType.Assembly.GetFirstOrDefaultImplementationTypeOf(serviceType);
            Assert.Equal(implementationType, result);
        }
    }
}