using Photosphere.DependencyInjection.Attributes;
using Photosphere.DependencyInjection.IntegrationTests.RootAttributeRegistration.TestObjects;
using Xunit;

[assembly: CompositionRoot(typeof(FooCompositionRoot))]

namespace Photosphere.DependencyInjection.IntegrationTests.RootAttributeRegistration
{
    public class CompositionRootAttributeRegistrationTests
    {
        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttribute_NotNull()
        {
            using (var container = new DependencyContainer())
            {
                var result = container.GetInstance<Foo>();
                Assert.NotNull(result);
            }
        }
    }
}