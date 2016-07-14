using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistrySaturator : IRegistrySaturator
    {
        private readonly IRegistry _registry;
        private readonly IScopeKeeper _scopeKeeper;

        public RegistrySaturator(IRegistry registry, IScopeKeeper scopeKeeper)
        {
            _registry = registry;
            _scopeKeeper = scopeKeeper;
        }

        public void Saturate()
        {
            foreach (var registration in _registry)
            {
                _scopeKeeper.StartNewPerRequestScope();
                registration.GenerateInstantiateFunction();
            }
        }
    }
}