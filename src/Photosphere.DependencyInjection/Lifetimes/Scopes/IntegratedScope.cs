using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes
{
    internal class IntegratedScope : IIntegratedScope
    {
        private IDictionary<Type, LocalBuilder> _availableInstancesVariables;

        public IntegratedScope()
        {
            _availableInstancesVariables = new ConcurrentDictionary<Type, LocalBuilder>();
        }

        public IReadOnlyDictionary<Type, LocalBuilder> AvailableInstancesVariables
            => (ConcurrentDictionary<Type, LocalBuilder>) _availableInstancesVariables;

        public void Add(Type type, LocalBuilder instanceVariable)
        {
            _availableInstancesVariables.Add(type, instanceVariable);
        }

        public void Dispose()
        {
            _availableInstancesVariables = null;
        }
    }
}