using Photosphere.DependencyInjection.xUnit.IntegrationTests.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.xUnit.IntegrationTests
{
    public class InjectDependencyTests
    {
        [Theory]
        [InjectDependency]
        internal void GetData_ByClassType_NotNull(Foo foo)
        {
            Assert.NotNull(foo);
        }

        [Theory]
        [InjectDependency]
        internal void GetData_ByInterface_NotNull(IFoo foo)
        {
            Assert.NotNull(foo);
        }

        [Theory]
        [InjectDependency]
        internal void GetData_ByInterface_SecondLevelDependencyNotNull(IFoo foo)
        {
            Assert.NotNull(foo.Bar);
        }

        [Theory]
        [InjectDependency]
        internal void GetData_FewParams_NotNull(IFoo foo, IBar bar)
        {
            Assert.NotNull(foo);
            Assert.NotNull(bar);
        }

        [Theory]
        [InjectDependency(42, 4.2, "foo")]
        internal void GetData_FewParamsWithData_NotNull(int number1, double number2, string str, IFoo foo, IBar bar)
        {
            Assert.Equal(42, number1);
            Assert.Equal(4.2, number2);
            Assert.Equal("foo", str);
            Assert.NotNull(foo);
            Assert.NotNull(bar);
        }
    }
}