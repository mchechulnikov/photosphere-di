using System.Reflection;
using Photosphere.DependencyInjection.TestAssembly.RootAttributeRegistration.Objects;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests.Tests
{
    public class CompositionRootAttributeRegistrationTests
    {
        private readonly Assembly _targetAssembly = typeof(CompositionRootAttributeRegistrationService).Assembly;

        [Fact]
        internal void GetInstance_CompositionRootSpecifiedByAttribute_NotNull()
        {
            using (var container = new DependencyContainer(_targetAssembly))
            {
                var result = container.GetInstance<CompositionRootAttributeRegistrationService>();
                Assert.NotNull(result);
            }
        }
    }
}