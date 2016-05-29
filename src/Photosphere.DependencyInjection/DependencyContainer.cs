using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.Services;
using Photosphere.DependencyInjection.Registrations.ValueObjects;
using Photosphere.DependencyInjection.Resolving;

namespace Photosphere.DependencyInjection
{
    public class DependencyContainer : IDependencyContainer
    {
        private readonly IRegistryInitializer _registryInitializer;
        private readonly IResolver _resolver;
        private readonly IScopeKeeper _scopeKeeper;

        public DependencyContainer()
        {
            var registry = new Registry();
            _scopeKeeper = new ScopeKeeper();
            _registryInitializer = new RegistryInitializer(registry, _scopeKeeper);
            _resolver = new Resolver(registry);
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