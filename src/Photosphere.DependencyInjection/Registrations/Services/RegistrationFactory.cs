using System;
using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistrationFactory : IRegistrationFactory
    {
        private readonly IInstantiateMethodGenerator _methodGenerator;

        public RegistrationFactory(IInstantiateMethodGenerator methodGenerator)
        {
            _methodGenerator = methodGenerator;
        }

        public IReadOnlyList<IRegistration> Get<TService>(Lifetime lifetime)
        {
            return typeof(TService)
                .GetAllDerivedTypes()
                .Select(serviceType => GetRegistration(lifetime, serviceType))
                .Where(r => r != null)
                .ToList();
        }

        private IRegistration GetRegistration(Lifetime lifetime, Type serviceType)
        {
            var implementationType = serviceType.GetFirstImplementationType();
            if (implementationType == null)
            {
                return null;
            }
            return new Registration(() => _methodGenerator.Generate(serviceType))
            {
                ServiceType = serviceType,
                ImplementationType = implementationType,
                Lifetime = lifetime
            };
        }
    }
}