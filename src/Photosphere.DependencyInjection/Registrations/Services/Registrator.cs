using System;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class Registrator : IRegistrator
    {
        private readonly IRegistry _registry;
        private readonly IRegistrationFactory _registrationFactory;

        public Registrator(
            IRegistry registry,
            IRegistrationFactory registrationFactory)
        {
            _registry = registry;
            _registrationFactory = registrationFactory;
        }

        public IRegistrator Register<TService>(Lifetime lifetime = Lifetime.PerRequest)
        {
            Register(typeof(TService), lifetime);
            return this;
        }

        public IRegistrator Register(Type setviceType, Lifetime lifetime = Lifetime.PerRequest)
        {
            var registrations = _registrationFactory.Get(setviceType, lifetime);
            _registry.Add(registrations);
            return this;
        }
    }
}