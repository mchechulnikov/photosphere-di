using System.Collections.Generic;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal interface IRegistrationFactory
    {
        IReadOnlyList<IRegistration> Get<TService>(Lifetime lifetime);
    }
}