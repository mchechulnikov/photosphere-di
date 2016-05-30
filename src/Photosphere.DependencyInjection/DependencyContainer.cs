using Photosphere.DependencyInjection.InnerStructure;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.Services;
using Photosphere.DependencyInjection.Resolving;

namespace Photosphere.DependencyInjection
{
    public class DependencyContainer : IDependencyContainer
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IResolver _resolver;
        private readonly IRegistryInitializer _registryInitializer;

        public DependencyContainer()
        {
            var serviceLocator = new InnerServiceLocator();
            _scopeKeeper = serviceLocator.ScopeKeeper;
            _resolver = serviceLocator.Resolver;
            _registryInitializer = serviceLocator.RegistryInitializer;
        }

        public void Initialize()
        {
            _registryInitializer.Initialize();
        }

        public TService GetInstance<TService>()
        {
            return _resolver.GetInstance<TService>();
        }

        public void Dispose()
        {
            _scopeKeeper.Dispose();
        }
    }
}