using System;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Resolving
{
    internal class Resolver : IResolver
    {
        private readonly IRegistry _registry;

        public Resolver(IRegistry registry)
        {
            _registry = registry;
        }

        public TService GetInstance<TService>()
        {
            var registration = _registry[typeof(TService)];
            TService result;
            switch (registration.Lifetime)
            {
                case Lifetime.AlwaysNew:
                case Lifetime.PerRequest:
                    result = ((Func<TService>)registration.InstantiateFunction).Invoke();
                    break;
                case Lifetime.PerContainer:
                    result = (TService) registration.Instance;
                    break;
                default:
                    throw new InvalidOperationException("Not supported lifetime");
            }
            return result;
        }
    }
}