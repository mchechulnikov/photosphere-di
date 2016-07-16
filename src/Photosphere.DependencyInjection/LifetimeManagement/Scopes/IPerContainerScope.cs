using System;
using System.Collections.Concurrent;

namespace Photosphere.DependencyInjection.LifetimeManagement.Scopes
{
    internal interface IPerContainerScope : IScope
    {
        object[] AvailableInstances { get; }

        ConcurrentDictionary<Type, int> AvailableInstancesIndexes { get; }
    }
}