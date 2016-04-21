using System;
using Photosphere.DependencyInjection.Extensions;
using Xunit;

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

    internal class Foo {}

    internal class Bar { public Bar() {} }

    internal class Qiz
    {
        private readonly Bar _bar;

        public Qiz(Bar bar)
        {
            _bar = bar;
        }
    }
}