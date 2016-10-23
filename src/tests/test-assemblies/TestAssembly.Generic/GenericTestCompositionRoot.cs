using Photosphere.DependencyInjection;
using TestAssembly.Generic.Generic;

namespace TestAssembly.Generic
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