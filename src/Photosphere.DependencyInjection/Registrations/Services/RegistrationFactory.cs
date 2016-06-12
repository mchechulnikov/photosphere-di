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

        public IEnumerable<IRegistration> Get<TService>(Lifetime lifetime)
        {
            var derivedTypes = typeof(TService).GetAllDerivedTypes().ToHashSet();
            foreach (var serviceType in derivedTypes)
            {
                var implementationTypes = derivedTypes.GetTypesThatImplements(serviceType).ToHashSet();
                if (implementationTypes.IsNullOrEmpty())
                {
                    continue;
                }
                yield return GetRegistration(lifetime, serviceType, implementationTypes);
                if (implementationTypes.HasSeveralElements())
                {
                    yield return GetRegistration(lifetime, serviceType, implementationTypes, true);
                }
            }
        }

        private Registration GetRegistration(Lifetime lifetime, Type originalServiceType, IReadOnlyCollection<Type> implementationTypes, bool isEnumerable = false)
        {
            Type serviceType, directImplementationType;
            if (isEnumerable)
            {
                serviceType = originalServiceType.MakeGenericWrappedBy(typeof(IEnumerable<>));
                directImplementationType = originalServiceType.MakeGenericWrappedBy(typeof(List<>));
            }
            else
            {
                serviceType = originalServiceType;
                directImplementationType = implementationTypes.First();
            }
            return new Registration(() => _methodGenerator.Generate(serviceType))
            {
                ServiceType = serviceType,
                DirectImplementationType = directImplementationType,
                ImplementationTypes = implementationTypes,
                IsEnumerable = isEnumerable,
                Lifetime = lifetime
            };
        }
    }
}