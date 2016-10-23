using TestAssembly.RegisterThroughAttribute;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Registration
{
    public class GetInstanceRegistrationByAttributeTests : IntegrationTestsBase
    {
        public GetInstanceRegistrationByAttributeTests() : base(typeof(Foo)) {}

        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttributes_NotNull()
        {
            var container = NewContainer;

            var foo = container.GetInstance<Foo>();
            var bar = container.GetInstance<Bar>();

            Assert.NotNull(foo);
            Assert.NotNull(bar);
        }
    }
}