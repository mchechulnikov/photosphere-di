using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistrationFactory : IRegistrationFactory
    {
        private readonly IRegistry _registry;

        public RegistrationFactory(IRegistry registry)
        {
            _registry = registry;
        }

        public IRegistration Get<TService>(Lifetime lifetime)
        {
            return new Registration
            {
                ServiceType = typeof(TService),
                ImplementationType = typeof(TService).GetFirstImplementationType(),
                InstantiateFunction = InstantiateMethodGenerator.Generate<TService>(_registry),
                Lifetime = lifetime
            };
        }
    }
}