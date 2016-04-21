using Photosphere.DependencyInjection.CilEmitting;
using Photosphere.DependencyInjection.Registration.ValueObjects;

namespace Photosphere.DependencyInjection.Registration.Services
{
    internal class Registrator : IRegistrator
    {
        private readonly IRegistry _registry;
        private readonly IValidator _validator;

        public Registrator(
            IRegistry registry,
            IValidator validator)
        {
            _registry = registry;
            _validator = validator;
        }

        public IRegistrator Register<TService>()
        {
            _validator.Validate<TService, TService>();
            _registry.Add(typeof(TService), InstantiateMethodGenerator.Generate<TService>());
            return this;
        }
    }
}