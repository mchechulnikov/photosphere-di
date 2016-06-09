using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests
{
    public class RegisteringByClassTests
    {
        [Fact]
        internal void GetInstance_ByClass_NotNull()
        {
            var contaner = new DependencyContainer();

            var foo = contaner.GetInstance<AlwaysNewFoo>();

            Assert.NotNull(foo);
        }
    }
}