using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistrationFactory : IRegistrationFactory
    {
        private readonly IInstantiateMethodGenerator _methodGenerator;

        public RegistrationFactory(IInstantiateMethodGenerator methodGenerator)
        {
            _methodGenerator = methodGenerator;
        }

        public IRegistration Get<TService>(Lifetime lifetime)
        {
            var serviceType = typeof(TService);
            return new Registration(() => _methodGenerator.Generate(serviceType))
            {
                ServiceType = serviceType,
                ImplementationType = serviceType.GetFirstImplementationType(),
                Lifetime = lifetime
            };
        }
    }
}