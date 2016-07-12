using System;
using Photosphere.DependencyInjection.Extensions;
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

        public IRegistrator Register(Type serviceType, Lifetime lifetime = Lifetime.PerRequest)
        {
            var registrations = _registrationFactory.Get(serviceType, lifetime);
            _registry.Add(registrations);
            return this;
        }

        public IRegistrator RegisterBy<TAttribute>(Lifetime lifetime = Lifetime.PerRequest) where TAttribute : Attribute
        {
            RegisterBy(typeof(TAttribute), lifetime);
            return this;
        }

        public IRegistrator RegisterBy(Type attributeType, Lifetime lifetime = Lifetime.PerRequest)
        {
            foreach (var serviceType in attributeType.GetMarkedTypes())
            {
                Register(serviceType, lifetime);
            }
            return this;
        }
    }
}