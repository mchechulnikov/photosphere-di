using Photosphere.DependencyInjection.Attributes;
using Photosphere.DependencyInjection.IntegrationTests.RegisterByAttribute.TestObjects;
using Photosphere.DependencyInjection.Lifetimes;
using Xunit;

[assembly: RegisterDependencies(typeof(Foo))]
[assembly: RegisterDependencies(typeof(Bar), Lifetime.AlwaysNew)]

namespace Photosphere.DependencyInjection.IntegrationTests.RegisterByAttribute
{
    public class RegisterByAttributeTests
    {
        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttributes_NotNull()
        {
            using (var container = new DependencyContainer())
            {
                var foo = container.GetInstance<Foo>();
                var bar = container.GetInstance<Bar>();

                Assert.NotNull(foo);
                Assert.NotNull(bar);
            }
        }
    }
}