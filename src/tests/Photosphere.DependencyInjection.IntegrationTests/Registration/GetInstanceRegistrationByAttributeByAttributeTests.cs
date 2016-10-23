using TestAssembly.RegisterByAttribute.TestObjects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Registration
{
    public class GetInstanceRegistrationByAttributeByAttributeTests : IntegrationTestsBase
    {
        public GetInstanceRegistrationByAttributeByAttributeTests() : base(typeof(FooForAttribute)) {}

        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttributesByAttribute_NotNull()
        {
            var result = NewContainer.GetInstance<FooForAttribute>();
            Assert.NotNull(result);
        }
    }
}