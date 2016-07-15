using Photosphere.DependencyInjection.TestAssembly.CommonInterface.TestObjects;

namespace Photosphere.DependencyInjection.TestAssembly.CommonInterface
{
    internal class CommonInterfaceTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.Register<IService>();
        }
    }
}