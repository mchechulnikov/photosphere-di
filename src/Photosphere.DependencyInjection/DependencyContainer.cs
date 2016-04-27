using System;
using Photosphere.DependencyInjection.CilEmitting;
using Photosphere.DependencyInjection.Registrations.Services;
using Photosphere.DependencyInjection.Resolving;

namespace Photosphere.DependencyInjection
{
    public class DependencyContainer : IDependencyContainer
    {
        private static readonly Func<IRegistryInitializer> RegistryInitializerInstantiator;
        private static readonly Func<IResolver> ResolverInstantiator;
        private readonly IRegistryInitializer _registryInitializer;
        private readonly IResolver _resolver;

        static DependencyContainer()
        {
            RegistryInitializerInstantiator = InstantiateMethodGenerator.Generate<IRegistryInitializer>();
            ResolverInstantiator = InstantiateMethodGenerator.Generate<IResolver>();
        }

        public DependencyContainer()
        {
            _registryInitializer = RegistryInitializerInstantiator.Invoke();
            _resolver = ResolverInstantiator.Invoke();
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