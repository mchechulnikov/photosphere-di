using Photosphere.DependencyInjection.CilEmitting;
using Photosphere.DependencyInjection.Registrations.Services;
using Photosphere.DependencyInjection.Resolving;

namespace Photosphere.DependencyInjection
{
    public class DependencyContainer : IDependencyContainer
    {
        private readonly IRegistryInitializer _registryInitializer;
        private readonly IResolver _resolver;

        public DependencyContainer()
        {
            _registryInitializer = InstantiateMethodGenerator.Generate<IRegistryInitializer>().Invoke();
            _resolver = InstantiateMethodGenerator.Generate<IResolver>().Invoke();
        }

        public void Initialize()
        {
            _registryInitializer.Initialize();
        }

        public TService GetInstance<TService>()
        {
            return _resolver.GetInstance<TService>();
        }
    }
}