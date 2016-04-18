namespace Photosphere.Registration.Services
{
    internal class RegistryInitializer : IRegistryInitializer
    {
        private readonly ICompositionRootFinder _compositionRootFinder;
        private readonly IRegistrator _registrator;

        public RegistryInitializer()
        {
            _compositionRootFinder = new CompositionRootFinder();
            _registrator = new Registrator();
        }

        public void Initialize()
        {
            var compositionRoots = _compositionRootFinder.Find();
            foreach (var compositionRoot in compositionRoots)
            {
                compositionRoot.Compose(_registrator);
            }
        }
    }
}