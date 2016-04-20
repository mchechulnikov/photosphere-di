namespace Photosphere.Registration.Services
{
    internal class RegistryInitializer : IRegistryInitializer
    {
        private readonly ICompositionRootFinder _compositionRootFinder;
        private readonly IRegistrator _registrator;

        public RegistryInitializer(
            ICompositionRootFinder compositionRootFinder,
            IRegistrator registrator)
        {
            _compositionRootFinder = compositionRootFinder;
            _registrator = registrator;
        }

        public void Initialize()
        {
            foreach (var compositionRoot in _compositionRootFinder.Find())
            {
                compositionRoot.Compose(_registrator);
            }
        }
    }
}