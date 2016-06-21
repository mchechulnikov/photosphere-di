using System;
using System.Collections.Generic;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
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