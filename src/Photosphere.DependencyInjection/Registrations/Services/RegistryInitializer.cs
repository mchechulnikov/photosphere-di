using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistryInitializer : IRegistryInitializer
    {
        private readonly ICompositionRootProvider _compositionRootProvider;
        private readonly IRegistrator _registrator;

        public RegistryInitializer(IRegistry registry, IScopeKeeper scopeKeeper)
        {
            _compositionRootProvider = new CompositionRootProvider();
            _registrator = new Registrator(registry, scopeKeeper);
        }

        public void Initialize()
        {
            foreach (var compositionRoot in _compositionRootProvider.Provide())
            {
                compositionRoot.Compose(_registrator);
            }
        }
    }
}