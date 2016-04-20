using System;
using Photosphere.CilEmitting;
using Photosphere.Registration.Services;
using Photosphere.Registration.ValueObjects;

namespace Photosphere
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IRegistryInitializer _registryInitializer;
        private readonly IRegistry _registry;

        public DependencyResolver()
        {
            _registryInitializer = InstantiateMethodGenerator.Generate<IRegistryInitializer>().Invoke();
            _registry = InstantiateMethodGenerator.Generate<IRegistry>().Invoke();
        }

        public void Initialize()
        {
            _registryInitializer.Initialize();
        }

        public TService GetInstance<TService>()
        {
            var instantiateMethod = (Func<TService>) _registry[typeof(TService)];
            return instantiateMethod.Invoke();
        }
    }
}