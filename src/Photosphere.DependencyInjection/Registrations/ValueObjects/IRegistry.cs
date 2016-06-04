using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal interface IRegistry : IEnumerable<IRegistration>
    {
        void Add(IRegistration registration);

        bool Contains(Type serviceType);

        IRegistration this[Type serviceType] { get; }
    }
}