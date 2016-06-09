using System;
using System.Collections.Generic;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Resolving
{
    internal class Resolver : IResolver
    {
        private readonly IRegistry _registry;
        private readonly IScopeKeeper _scopeKeeper;

        public Resolver(IRegistry registry, IScopeKeeper scopeKeeper)
        {
            _registry = registry;
            _scopeKeeper = scopeKeeper;
        }

        public TService GetInstance<TService>()
        {
            return Get<TService>();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return Get<IEnumerable<TService>>();
        }

        private T Get<T>()
        {
            var registration = _registry[typeof(T)];
            var instantiateFunction = (Func<object[], T>) registration.InstantiateFunction;
            return instantiateFunction.Invoke(_scopeKeeper.PerContainerScope.AvailableInstances);
        }
    }
}