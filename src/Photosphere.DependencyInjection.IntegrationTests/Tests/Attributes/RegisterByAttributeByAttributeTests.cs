using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.RegisterByAttribute;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests.Attributes
{
    public class RegisterByAttributeByAttributeTests
    {
        private readonly Assembly _targetAssembly = typeof(FooForAttribute).Assembly;

        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttributesByAttribute_NotNull()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var foo = container.GetInstance<FooForAttribute>();
                Assert.NotNull(foo);
            }
        }
    }
}