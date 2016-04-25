using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal interface IRegistrationFactory
    {
        IRegistration Get<TService>(Lifetime lifetime);
    }
}