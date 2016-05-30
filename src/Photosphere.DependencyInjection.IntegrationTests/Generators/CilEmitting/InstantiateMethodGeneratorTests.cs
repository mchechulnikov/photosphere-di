using Photosphere.DependencyInjection.Generators;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions;
using Photosphere.DependencyInjection.IntegrationTests.TestObjects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Generators.CilEmitting
{
    public class InstantiateMethodGeneratorTests
    {
        [Fact]
        internal void GenerateFor_Class_MethodNotNull()
        {
            var container = new DependencyContainer();
            var result = container.GetInstance<Foo>();
            Assert.NotNull(result);
        }

        //[Fact]
        //internal void GenerateFor_Interface_MethodNotNull()
        //{
        //    var result = InstantiateMethodGenerator.Generate<IFoo>();
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //internal void GenerateFor_Class_ResultNotNull()
        //{
        //    var result = InstantiateMethodGenerator.Generate<Foo>()();
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //internal void GenerateFor_Interface_ResultNotNull()
        //{
        //    var result = InstantiateMethodGenerator.Generate<IFoo>()();
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //internal void GenerateFor_Interface_ResultCorrectType()
        //{
        //    var result = InstantiateMethodGenerator.Generate<IFoo>()();
        //    Assert.Equal(typeof(Foo), result.GetType());
        //}

        //[Fact]
        //internal void GenerateFor_WithParameterlessNonDefaultConstructor_ResultNotNull()
        //{
        //    var result = InstantiateMethodGenerator.Generate<IBar>()();
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //internal void GenerateFor_WithDependencies_ResultNotNull()
        //{
        //    var result = InstantiateMethodGenerator.Generate<IQiz>()();
        //    Assert.NotNull(result);
        //}

        //[Fact]
        //internal void GenerateFor_WithDependencies_ResultCorrectType()
        //{
        //    var result = InstantiateMethodGenerator.Generate<IQiz>()();
        //    Assert.Equal(typeof(Qiz), result.GetType());
        //}

        //[Fact]
        //internal void GenerateFor_WithCircleDependencies_Exception()
        //{
        //    Assert.Throws<DetectedCircleDependencyException>(() => InstantiateMethodGenerator.Generate<ICircleFoo>());
        //}
    }
}