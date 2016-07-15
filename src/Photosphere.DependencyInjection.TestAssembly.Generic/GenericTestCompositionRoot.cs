using Photosphere.DependencyInjection.TestAssembly.Generic.Generic;

namespace Photosphere.DependencyInjection.TestAssembly.Generic
{
    internal class GenericTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register(typeof(IGenericService<>))
                .Register(typeof(GenericServiceClass<>));
        }
    }
}