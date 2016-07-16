using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Attributes;

namespace Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots.ServiceCompositionRoots
{
    internal class DefaultCompositionRoot : ICompositionRoot
    {
        private readonly IEnumerable<RegisterDependenciesAttribute> _registerAttributes;
        private readonly IEnumerable<RegisterDependenciesByAttribute> _registerByAttributes;

        public DefaultCompositionRoot(
            IEnumerable<RegisterDependenciesAttribute> registerAttributes,
            IEnumerable<RegisterDependenciesByAttribute> registerByAttributes,
            Assembly targetAssembly)
        {
            _registerAttributes = registerAttributes ?? new List<RegisterDependenciesAttribute>();
            _registerByAttributes = registerByAttributes ?? new List<RegisterDependenciesByAttribute>();
            TargetAssembly = targetAssembly;
        }

        public Assembly TargetAssembly { get; }

        public void Compose(IRegistrator registrator)
        {
            foreach (var attribute in _registerAttributes)
            {
                registrator.Register(attribute.ServiceType, attribute.Lifetime);
            }
            foreach (var attribute in _registerByAttributes)
            {
                registrator.RegisterBy(attribute.RegistrationAttributeType, attribute.Lifetime);
            }
        }
    }
}