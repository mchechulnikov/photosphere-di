using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots.ServiceCompositionRoots
{
    internal class DefaultCompositionRoot : ICompositionRoot
    {
        private readonly IEnumerable<Type> _serviceTypes;
        private readonly IEnumerable<Type> _registrationAttributesTypes;

        public DefaultCompositionRoot(
            IEnumerable<Type> serviceTypes,
            IEnumerable<Type> registrationAttributesTypes,
            Assembly targetAssembly)
        {
            _serviceTypes = serviceTypes ?? new List<Type>();
            _registrationAttributesTypes = registrationAttributesTypes ?? new List<Type>();
            TargetAssembly = targetAssembly;
        }

        public Assembly TargetAssembly { get; }

        public void Compose(IRegistrator registrator)
        {
            foreach (var serviceType in _serviceTypes)
            {
                registrator.Register(serviceType);
            }
            foreach (var attribute in _registrationAttributesTypes)
            {
                registrator.RegisterBy(attribute);
            }
        }
    }
}