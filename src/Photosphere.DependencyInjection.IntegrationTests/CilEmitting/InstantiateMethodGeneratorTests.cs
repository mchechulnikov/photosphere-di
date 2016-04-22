using Photosphere.DependencyInjection.CilEmitting;
using Photosphere.DependencyInjection.TestUtils;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.CilEmitting
{
    public class InstantiateMethodGeneratorTests
    {
        [Fact]
        internal void GenerateFor_Class_MethodNotNull()
        {
            var result = InstantiateMethodGenerator.Generate<Foo>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GenerateFor_Interface_MethodNotNull()
        {
            var result = InstantiateMethodGenerator.Generate<IFoo>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GenerateFor_Class_ResultNotNull()
        {
            var result = InstantiateMethodGenerator.Generate<Foo>()();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GenerateFor_Interface_ResultNotNull()
        {
            var result = InstantiateMethodGenerator.Generate<IFoo>()();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GenerateFor_WithDependencies_ResultNotNull()
        {
            var result = InstantiateMethodGenerator.Generate<IQiz>()();
            Assert.NotNull(result);
        }
    }
}