using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal interface IRegistry : IEnumerable<IRegistration>
    {
        void Add(IRegistration registration);

        bool Contains(Type type);

        IRegistration this[Type type] { get; }
    }
}