using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services
{
    internal class RegistrationFactory : IRegistrationFactory
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IInstantiateMethodGenerator _methodGenerator;

        public RegistrationFactory(
            IScopeKeeper scopeKeeper,
            IInstantiateMethodGenerator methodGenerator)
        {
            _scopeKeeper = scopeKeeper;
            _methodGenerator = methodGenerator;
        }

        public IRegistration Get<TService>(Lifetime lifetime)
        {
            _scopeKeeper.StartNewScope();
            return new Registration(() => _methodGenerator.Generate<TService>())
            {
                ServiceType = typeof(TService),
                ImplementationType = typeof(TService).GetFirstImplementationType(),
                Lifetime = lifetime
            };
        }
    }
}