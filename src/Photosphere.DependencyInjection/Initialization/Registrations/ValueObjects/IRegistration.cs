using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects
{
    internal interface IRegistration
    {
        Type ServiceType { get; }

        Type DirectImplementationType { get; }

        IReadOnlyCollection<Type> ImplementationTypes { get; }

        Delegate InstanceProvidingFunction { get; }

        bool IsEnumerable { get; }

        Lifetime Lifetime { get; }

        void GenerateInstantiateFunction();
    }
}