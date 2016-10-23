using System;
using Xunit;
using Photosphere.DependencyInjection.UnitTests.TestObjects.Objects;
using Photosphere.DependencyInjection.Extensions;

namespace Photosphere.DependencyInjection.UnitTests.Extensions
{
    public class TypeExtensionsTests
    {
        [Theory]
        [InlineData(typeof(Foo))]
        [InlineData(typeof(Bar))]
        [InlineData(typeof(Qiz))]
        public void GetFirstPublicConstructor_DefaultConstructor_NotNull(Type type)
        {
            var result = type.GetFirstPublicConstructor();
            Assert.NotNull(result);
        }
    }
}