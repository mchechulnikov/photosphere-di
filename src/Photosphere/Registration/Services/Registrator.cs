using Photosphere.CilEmitting.Services;
using Photosphere.Registration.ValueObjects;

namespace Photosphere.Registration.Services
{
    internal class Registrator : IRegistrator
    {
        private readonly IRegistry _registry;
        private readonly IValidator _validator;
        private readonly IInstantiateMethodGenerator _methodGenerator;

        public Registrator(
            IRegistry registry,
            IValidator validator,
            IInstantiateMethodGenerator methodGenerator)
        {
            _registry = registry;
            _validator = validator;
            _methodGenerator = methodGenerator;
        }

        public IRegistrator Register<TService, TImplementation>()
        {
            _validator.Validate<TService, TImplementation>();
            _registry.Add(typeof(TService), _methodGenerator.Generate<TImplementation>());
            return this;
        }

        public IRegistrator Register<TService>()
        {
            throw new System.NotImplementedException();
        }
    }
}