using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class Registrator : IRegistrator
    {
        private readonly IRegistry _registry;
        private readonly IValidator _validator;
        private readonly IRegistrationFactory _registrationFactory;

        public Registrator(
            IRegistry registry,
            IValidator validator,
            IRegistrationFactory registrationFactory)
        {
            _registry = registry;
            _validator = validator;
            _registrationFactory = registrationFactory;
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