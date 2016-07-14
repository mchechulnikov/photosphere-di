using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generation;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistrationFactory : IRegistrationFactory
    {
        private readonly IInstanceProvidingMethodGenerator _methodGenerator;

        public RegistrationFactory(
            IInstanceProvidingMethodGenerator methodGenerator)
        {
            _methodGenerator = methodGenerator;
        }

        public IEnumerable<IRegistration> Get(Type serviceType, Assembly assembly, Lifetime lifetime)
        {
            var derivedTypes = serviceType.GetAllDerivedTypesFrom(assembly).ToHashSet();
            return GetRegistrations(lifetime, derivedTypes);
        }

        public IEnumerable<IRegistration> GetByAttribute(Type attributeType, Assembly assembly, Lifetime lifetime)
        {
            var markedTypes = attributeType.GetMarkedTypes();
            var servicesTypes =
                markedTypes
                .Where(t => t.IsInterface)
                .SelectMany(t => t.GetAllDerivedTypesFrom(assembly))
                .Union(markedTypes)
                .ToHashSet();

            return GetRegistrations(lifetime, servicesTypes).ToList();
        }

        private IEnumerable<IRegistration> GetRegistrations(Lifetime lifetime, ISet<Type> servicesTypes)
        {
            foreach (var serviceType in servicesTypes)
            {
                var implementationTypes = servicesTypes.GetTypesThatImplements(serviceType).ToHashSet();
                if (implementationTypes.IsNullOrEmpty())
                {
                    continue;
                }
                yield return GetRegistration(lifetime, serviceType, implementationTypes);
                if (implementationTypes.HasNonSeveralElements())
                {
                    continue;
                }
                yield return GetRegistration(lifetime, serviceType, implementationTypes, typeof(IEnumerable<>));
                yield return GetRegistration(lifetime, serviceType, implementationTypes, typeof(IReadOnlyCollection<>));
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
                directImplementationType = originalServiceType.IsInstantiatible() ? originalServiceType : implementationTypes.First();
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