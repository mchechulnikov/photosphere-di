using Photosphere.CilEmitting.Services;
using Photosphere.Registration.ValueObjects;

namespace Photosphere.Registration.Services
{
    internal class Registrator : IRegistrator
    {
        private readonly IRegistry _registry;
        private readonly IInstantiateMethodGenerator _methodGenerator;

        public Registrator(IRegistry registry, IInstantiateMethodGenerator methodGenerator)
        {
            _registry = registry;
            _methodGenerator = methodGenerator;
        }

        public IRegistrator Register<TService, TImplementation>()
        {
            // TODO Validate types

            var instantiateMethod = _methodGenerator.Generate<TImplementation>();
            _registry.Add(typeof(TService), instantiateMethod);

            return this;
        }
    }
}