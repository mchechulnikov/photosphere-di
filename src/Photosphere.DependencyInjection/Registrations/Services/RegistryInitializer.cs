using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistryInitializer : IRegistryInitializer
    {
        private readonly ICompositionRootProvider _compositionRootProvider;
        private readonly IRegistrator _registrator;
        private readonly IRegistry _registry;
        private readonly IScopeKeeper _scopeKeeper;

        public RegistryInitializer(
            ICompositionRootProvider compositionRootProvider,
            IRegistrator registrator,
            IRegistry registry,
            IScopeKeeper scopeKeeper)
        {
            _compositionRootProvider = compositionRootProvider;
            _registrator = registrator;
            _registry = registry;
            _scopeKeeper = scopeKeeper;
        }

        public void Initialize()
        {
            ComposeDependencies();
            SetupRegistry();
        }

        private void ComposeDependencies()
        {
            foreach (var compositionRoot in _compositionRootProvider.Provide())
            {
                compositionRoot.Compose(_registrator);
            }
        }

        private void SetupRegistry()
        {
            foreach (var registration in _registry)
            {
                _scopeKeeper.StartNewPerRequestScope();
                registration.GenerateInstantiateFunction();
            }
        }
    }
}