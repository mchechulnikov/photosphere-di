using System;
using System.Collections.Concurrent;

namespace Photosphere.DependencyInjection.LifetimeManagement.Scopes
{
    internal class PerContainerScope : IPerContainerScope
    {
        private object[] _availableServices;

        public PerContainerScope()
        {
            AvailableInstancesIndexes = new ConcurrentDictionary<Type, int>();
        }

        public object[] AvailableInstances => _availableServices ?? (_availableServices = new object[AvailableInstancesIndexes.Count]);

        public ConcurrentDictionary<Type, int> AvailableInstancesIndexes { get; private set; }

        public void Dispose()
        {
            _availableServices = null;
            AvailableInstancesIndexes = null;
        }
    }
}