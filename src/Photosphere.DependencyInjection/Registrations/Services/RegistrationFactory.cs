using System;
using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generation;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistrationFactory : IRegistrationFactory
    {
        private readonly IInstanceProvidingMethodGenerator _methodGenerator;

        public RegistrationFactory(IInstanceProvidingMethodGenerator methodGenerator)
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
                    yield return GetRegistration(lifetime, serviceType, implementationTypes, typeof(IEnumerable<>));
                    yield return GetRegistration(lifetime, serviceType, implementationTypes, typeof(IReadOnlyCollection<>));
                }
            }
        }

        private Registration GetRegistration(
            Lifetime lifetime, Type originalServiceType, IReadOnlyCollection<Type> implementationTypes, Type genericWrapperType = null)
        {
            Type serviceType, directImplementationType;
            if (genericWrapperType != null)
            {
                serviceType = originalServiceType.MakeGenericWrappedBy(genericWrapperType);
                directImplementationType = originalServiceType.MakeArrayType(implementationTypes.Count);
                lifetime = Lifetime.AlwaysNew;
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
                IsEnumerable = genericWrapperType != null,
                Lifetime = lifetime
            };
        }
    }
}