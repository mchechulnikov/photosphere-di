using System.Collections.Generic;
using Photosphere.DependencyInjection.InnerStructure;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Resolving;

namespace Photosphere.DependencyInjection
{
    public class DependencyContainer : IDependencyContainer
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IResolver _resolver;

        public DependencyContainer()
        {
            var serviceLocator = new InnerServiceLocator();
            _scopeKeeper = serviceLocator.ScopeKeeper;
            _resolver = serviceLocator.Resolver;

            serviceLocator.RegistryInitializer.Initialize();
        }

        public TService GetInstance<TService>()
        {
            return _resolver.GetInstance<TService>();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _resolver.GetAllInstances<TService>();
        }

        public void Dispose()
        {
            _scopeKeeper.Dispose();
        }
    }
}