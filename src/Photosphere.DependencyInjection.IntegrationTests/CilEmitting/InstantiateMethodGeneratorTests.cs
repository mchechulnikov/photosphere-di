using Photosphere.DependencyInjection.CilEmitting;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.CilEmitting
{
    public class InstantiateMethodGeneratorTests
    {
        [Fact]
        public void GenerateFor_Class_InstantiateMethodNotNull()
        {
            var result = InstantiateMethodGenerator.Generate<Foo>();
            Assert.NotNull(result);
        }

        // TODO [Fact]
        public void GenerateFor_Interface_InstantiateMethodNotNull()
        {
            var result = InstantiateMethodGenerator.Generate<IFoo>();
            Assert.NotNull(result);
        }

        [Fact]
        public void GenerateFor_Class_ResultNotNull()
        {
            var method = InstantiateMethodGenerator.Generate<Foo>();
            var result = method();
            Assert.NotNull(result);
        }
    }

    public interface IFoo {}
    public class Foo : IFoo {}
}