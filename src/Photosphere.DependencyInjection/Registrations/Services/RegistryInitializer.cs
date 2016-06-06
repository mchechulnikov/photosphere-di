using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistryInitializer : IRegistryInitializer
    {
        private readonly ICompositionRootProvider _compositionRootProvider;
        private readonly IRegistrator _registrator;
        private readonly IRegistry _registry;

        public RegistryInitializer(
            ICompositionRootProvider compositionRootProvider,
            IRegistrator registrator,
            IRegistry registry)
        {
            _compositionRootProvider = compositionRootProvider;
            _registrator = registrator;
            _registry = registry;
        }

        public void Initialize()
        {
            foreach (var compositionRoot in _compositionRootProvider.Provide())
            {
                compositionRoot.Compose(_registrator);
            }
            foreach (var registration in _registry)
            {
                registration.GenerateInstantiateFunction();
            }
        }
    }
}