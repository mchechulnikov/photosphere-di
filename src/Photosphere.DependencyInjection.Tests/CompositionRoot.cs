using Photosphere.DependencyInjection;
using Photosphere.Tests.TestServices;

namespace Photosphere.Tests
{
    internal class CompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IFoo>()
                .Register<IBar>();
        }
    }
}