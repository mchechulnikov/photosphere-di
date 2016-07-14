using System;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class AssemblyBoundedRegistrator : IAssemblyBoundedRegistrator
    {
        private readonly IRegistry _registry;
        private readonly IRegistrationFactory _registrationFactory;

        public AssemblyBoundedRegistrator(
            IRegistry registry,
            IRegistrationFactory registrationFactory)
        {
            _registry = registry;
            _registrationFactory = registrationFactory;
        }

        public void Register(Type serviceType, Assembly assembly, Lifetime lifetime)
        {
            var registrations = _registrationFactory.Get(serviceType, assembly, lifetime);
            _registry.Add(registrations);
        }

        public void RegisterBy(Type attributeType, Assembly assembly, Lifetime lifetime)
        {
            if (!attributeType.IsAttribute())
            {
                throw new ArgumentException($"`{attributeType.FullName}` is not attribute");
            }
            var registrations = _registrationFactory.GetByAttribute(attributeType, assembly, lifetime);
            _registry.Add(registrations);
        }
    }
}