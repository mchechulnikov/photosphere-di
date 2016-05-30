using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistryInitializer : IRegistryInitializer
    {
        private readonly ICompositionRootProvider _compositionRootProvider;
        private readonly IRegistrator _registrator;

        public RegistryInitializer(
            ICompositionRootProvider compositionRootProvider, IRegistrator registrator)
        {
            _compositionRootProvider = compositionRootProvider;
;            _registrator = registrator;
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