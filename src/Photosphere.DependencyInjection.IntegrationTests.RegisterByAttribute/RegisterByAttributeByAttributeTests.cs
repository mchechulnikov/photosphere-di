using Photosphere.DependencyInjection.IntegrationTests.RegisterByAttribute.TestObjects;
using Photosphere.DependencyInjection.Registrations.Attributes;
using Xunit;

[assembly: RegisterDependenciesBy(typeof(TestRegisterAttribute))]

namespace Photosphere.DependencyInjection.IntegrationTests.RegisterByAttribute
{
    public class RegisterByAttributeByAttributeTests
    {
        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttributesByAttribute_NotNull()
        {
            using (var container = new DependencyContainer())
            {
                var foo = container.GetInstance<FooForAttribute>();
                Assert.NotNull(foo);
            }
        }
    }
}