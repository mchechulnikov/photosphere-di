using Photosphere.DependencyInjection.Generators;
using Photosphere.DependencyInjection.Registrations.Services;
using Photosphere.DependencyInjection.Resolving;
using Xunit;

namespace Photosphere.DependencyInjection.IntegrationTests
{
    public class SelfResolvingTests
    {
        [Fact]
        internal void Resolve_RegistryInitializer_NotNull()
        {
            Assert.NotNull(InstantiateMethodGenerator.Generate<IRegistryInitializer>().Invoke());
        }

        [Fact]
        internal void Resolve_Resolver_NotNull()
        {
            Assert.NotNull(InstantiateMethodGenerator.Generate<IResolver>().Invoke());
        }
    }
}