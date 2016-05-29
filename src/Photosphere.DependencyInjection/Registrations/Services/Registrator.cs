using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class Registrator : IRegistrator
    {
        private readonly IRegistry _registry;
        private readonly IValidator _validator;
        private readonly IRegistrationFactory _registrationFactory;

        public Registrator(IRegistry registry, IScopeKeeper scopeKeeper)
        {
            _registry = registry;
            _validator = new Validator();
            _registrationFactory = new RegistrationFactory(registry, scopeKeeper);
        }

        public IRegistrator Register<TService>(Lifetime lifetime = Lifetime.PerRequest)
        {
            _validator.Validate<TService, TService>();
            var registration = _registrationFactory.Get<TService>(lifetime);
            _registry.Add(registration);
            // TODO register implementation types as registrations
            return this;
        }
    }
}