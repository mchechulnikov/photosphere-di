using TestAssembly.CommonInterface.TestObjects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Registration
{
    public class GetInstanceRegistrationByCommonInterfaceTests : IntegrationTestsBase
    {
        public GetInstanceRegistrationByCommonInterfaceTests() : base(typeof(IService11)) {}

        [Fact]
        internal void GetInstance_ByFirstLevelInterface_NotNull()
        {
            var result = NewContainer.GetInstance<IService11>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByFirstLevelInterface_ExpectedType()
        {
            var result = NewContainer.GetInstance<IService11>();
            Assert.IsType<Service11>(result); ;
        }

        [Fact]
        internal void GetInstance_BySecondLevelInterface_NotNull()
        {
            var result = NewContainer.GetInstance<IService1>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_BySecondLevelInterface_ExpectedType()
        {
            var result = NewContainer.GetInstance<IService1>();
            Assert.True(result is Service11 || result is Service12);
        }

        [Fact]
        internal void GetInstance_ByThirdLevelInterface_NotNull()
        {
            var result = NewContainer.GetInstance<IService>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_ByThirdLevelInterface_ExpectedType()
        {
            var result = NewContainer.GetInstance<IService>();
            Assert.True(result is Service11 || result is Service12 || result is Service21 || result is Service22);
        }

        [Fact]
        internal void GetInstance_ByClass_NotNull()
        {
            var result = NewContainer.GetInstance<Service11>();
            Assert.NotNull(result);
        }

        [Fact]
        internal void GetInstance_WithClassDependency_ExpectedType()
        {
            var result = NewContainer.GetInstance<Service22>();
            Assert.IsType<Service21>(result.Service21);
        }
    }
}