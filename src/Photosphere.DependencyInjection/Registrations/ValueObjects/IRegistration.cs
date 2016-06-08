using System;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal interface IRegistration
    {
        Type ServiceType { get; }

        Type ImplementationType { get; }

        Delegate InstantiateFunction { get; }

        Lifetime Lifetime { get; }

        void GenerateInstantiateFunction();
    }
}