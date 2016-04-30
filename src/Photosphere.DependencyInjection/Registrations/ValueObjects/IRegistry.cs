using System;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal interface IRegistry
    {
        void Add(IRegistration registration);

        bool Contains(Type type);

        IRegistration this[Type type] { get; }
    }
}