using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Initialization;
using Photosphere.DependencyInjection.InnerStructure;
using Photosphere.DependencyInjection.LifetimeManagement;
using Photosphere.DependencyInjection.Resolving;

namespace Photosphere.DependencyInjection
{
    public class DependencyContainer : IDependencyContainer
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IResolver _resolver;

        public DependencyContainer(IContainerConfiguration configuration = null)
        {
            var serviceLocator = new ServiceLocator(configuration);

            _scopeKeeper = serviceLocator.Get<IScopeKeeper>();
            _resolver = serviceLocator.Get<IResolver>();

            serviceLocator.Get<IRegistryInitializer>().Initialize();
        }

        public DependencyContainer(params Assembly[] assemblies)
            : this(new ContainerConfiguration(assemblies)) {}

        public TService GetInstance<TService>()
        {
            return _resolver.GetInstance<TService>();
        }

        public object GetInstance(Type type)
        {
            return _resolver.GetInstance(type);
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