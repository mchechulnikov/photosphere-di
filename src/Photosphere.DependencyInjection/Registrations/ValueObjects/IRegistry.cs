using System;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal interface IRegistry
    {
        void Add(IRegistration registration);

        IRegistration this[Type type] { get; }
    }
}