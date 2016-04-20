using System;
using Photosphere.Lifetimes;

namespace Photosphere.Registration.ValueObjects
{
    internal interface IRegistration
    {
        Type ServiceType { get; }

        Type ImplementationType { get; }

        object Instance { get; }

        Lifetime Lifetime { get; }
    }
}