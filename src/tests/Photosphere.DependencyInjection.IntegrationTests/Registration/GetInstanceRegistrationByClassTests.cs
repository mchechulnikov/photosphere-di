using TestAssembly.Lifetimes.TestObjects;
using Xunit;

namespace Photosphere.Di.IntegrationTests.Registration
{
    public class GetInstanceRegistrationByClassTests : IntegrationTestsBase
    {
        public GetInstanceRegistrationByClassTests() : base(typeof(AlwaysNewFoo)) {}

        [Fact]
        internal void GetInstance_ByClass_NotNull()
        {
            var foo = NewContainer.GetInstance<AlwaysNewFoo>();
            Assert.NotNull(foo);
        }
    }
}