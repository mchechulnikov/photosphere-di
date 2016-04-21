using System;
using Photosphere.DependencyInjection.Lifetimes;

namespace Photosphere.DependencyInjection.Registration.ValueObjects
{
    internal interface IRegistration
    {
        Type ServiceType { get; }

        Type ImplementationType { get; }

        object Instance { get; }

        Lifetime Lifetime { get; }
    }
}