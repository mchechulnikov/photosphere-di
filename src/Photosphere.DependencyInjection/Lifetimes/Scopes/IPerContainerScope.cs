using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes
{
    internal interface IPerContainerScope : IScope
    {
        object[] AvailableInstances { get; }

        IDictionary<Type, int> AvailableInstancesIndexes { get; }
    }
}