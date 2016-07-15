using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.RegisterByAttribute;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.Attributes
{
    public class RegisterByAttributeTests
    {
        private static readonly Assembly TargetAssembly = typeof(Foo).Assembly;

        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttributes_NotNull()
        {
            using (var container = new DependencyContainer(TargetAssembly))
            {
                var foo = container.GetInstance<Foo>();
                var bar = container.GetInstance<Bar>();

                Assert.NotNull(foo);
                Assert.NotNull(bar);
            }
        }
    }
}