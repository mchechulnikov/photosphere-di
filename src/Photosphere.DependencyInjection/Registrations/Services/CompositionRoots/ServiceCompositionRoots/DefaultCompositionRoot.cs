using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots.ServiceCompositionRoots
{
    internal class DefaultCompositionRoot : ICompositionRoot
    {
        private readonly IEnumerable<Type> _serviceTypes;

        public DefaultCompositionRoot(IEnumerable<Type> serviceTypes)
        {
            _serviceTypes = serviceTypes;
        }

        public void Compose(IRegistrator registrator)
        {
            foreach (var serviceType in _serviceTypes)
            {
                registrator.Register(serviceType);
            }
        }
    }
}