using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation;
using Photosphere.DependencyInjection.Types;

namespace Photosphere.DependencyInjection.Initialization.Registrations
{
    internal class RegistrationFactory : IRegistrationFactory
    {
        private readonly ITypesProvider _typesProvider;
        private readonly IInstanceProvidingMethodGenerator _methodGenerator;

        public RegistrationFactory(
            ITypesProvider typesProvider,
            IInstanceProvidingMethodGenerator methodGenerator)
        {
            _typesProvider = typesProvider;
            _methodGenerator = methodGenerator;
        }

        public IEnumerable<IRegistration> Get(Type serviceType, Assembly assembly, Lifetime lifetime)
        {
            var derivedTypes = _typesProvider.GetAllDerivedTypesFrom(serviceType, assembly);
            return GetRegistrations(lifetime, derivedTypes);
        }

        public IEnumerable<IRegistration> GetByAttribute(Type attributeType, Assembly assembly, Lifetime lifetime)
        {
            var markedTypes = _typesProvider.GetMarkedTypes(attributeType);
            var servicesTypes =
                markedTypes
                .Where(t => t.IsInterface)
                .SelectMany(t => _typesProvider.GetAllDerivedTypesFrom(t, assembly))
                .Union(markedTypes)
                .ToHashSet();

            return GetRegistrations(lifetime, servicesTypes).ToList();
        }

        private IEnumerable<IRegistration> GetRegistrations(Lifetime lifetime, IReadOnlyCollection<Type> servicesTypes)
        {
            foreach (var serviceType in servicesTypes)
            {
                var implementationTypes =
                    servicesTypes.Where(t => t.IsInstantiatibleUserDefinedClass() && serviceType.IsAssignableFrom(t)).ToHashSet();
                if (implementationTypes.IsNullOrEmpty())
                {
                    continue;
                }
                yield return GetRegistration(lifetime, serviceType, implementationTypes);
                yield return GetRegistration(lifetime, serviceType, implementationTypes, typeof(IEnumerable<>));
                yield return GetRegistration(lifetime, serviceType, implementationTypes, typeof(IReadOnlyCollection<>));
            }
        }

        private Registration GetRegistration(
            Lifetime lifetime,
            Type originalServiceType,
            IReadOnlyCollection<Type> implementationTypes,
            Type genericWrapperType = null)
        {
            Type serviceType, directImplementationType;
            if (genericWrapperType != null)
            {
                serviceType = originalServiceType.MakeGenericWrappedBy(genericWrapperType);
                directImplementationType = originalServiceType.MakeArrayType();
                lifetime = Lifetime.AlwaysNew;
            }
            else
            {
                serviceType = originalServiceType;
                directImplementationType = originalServiceType.IsInstantiatible() ? originalServiceType : implementationTypes.First();
            }
            return new Registration(() => _methodGenerator.Generate(serviceType), implementationTypes.ToList())
            {
                ServiceType = serviceType,
                DirectImplementationType = directImplementationType,
                IsEnumerable = genericWrapperType != null,
                Lifetime = lifetime
            };
        }
    }
}