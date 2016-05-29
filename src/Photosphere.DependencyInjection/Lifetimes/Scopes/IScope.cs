using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes
{
    public interface IScope : IDisposable
    {
        IReadOnlyDictionary<Type, LocalBuilder> AvailableInstancesVariables { get; }
    }
}