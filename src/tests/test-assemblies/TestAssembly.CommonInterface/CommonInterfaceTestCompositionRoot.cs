using Photosphere.DependencyInjection;
using TestAssembly.CommonInterface.TestObjects;

namespace TestAssembly.CommonInterface
{
    internal class CommonInterfaceTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.Register<IService>();
        }
    }
}