using Photosphere.CilEmitting;
using Photosphere.Registration.ValueObjects;

namespace Photosphere.Registration.Services
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