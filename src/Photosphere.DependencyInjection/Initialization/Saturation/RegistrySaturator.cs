using Photosphere.DependencyInjection.Extensions;
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
            _registry.ParallelProceed(_registry.Count, Saturate);
        }

        private void Saturate(IRegistration registration)
        {
            _scopeKeeper.StartNewPerRequestScope();
            registration.GenerateInstantiateFunction();
        }
    }
}