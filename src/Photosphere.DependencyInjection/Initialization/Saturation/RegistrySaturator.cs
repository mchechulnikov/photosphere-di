using Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects;
using Photosphere.DependencyInjection.LifetimeManagement;

namespace Photosphere.DependencyInjection.Initialization.Saturation
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