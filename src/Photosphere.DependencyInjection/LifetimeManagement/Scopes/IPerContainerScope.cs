using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.LifetimeManagement.Scopes
{
    internal interface IPerContainerScope : IScope
    {
        object[] AvailableInstances { get; }

        IDictionary<Type, int> AvailableInstancesIndexes { get; }
    }
}