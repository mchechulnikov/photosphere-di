using TestAssembly.RootAttributeRegistration.Objects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Registration
{
    public class GetInstanceRegistrationByCompositionRootAttributeTests : IntegrationTestsBase
    {
        public GetInstanceRegistrationByCompositionRootAttributeTests()
            : base(typeof(CompositionRootAttributeRegistrationService)) {}

        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttribute_NotNull()
        {
            var result = NewContainer.GetInstance<CompositionRootAttributeRegistrationService>();
            Assert.NotNull(result);
        }
    }
}