using Photosphere.DependencyInjection.TestAssembly.RegisterByAttribute.TestObjects;

namespace Photosphere.DependencyInjection.TestAssembly.RegisterByAttribute
{
    internal class MainTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.RegisterBy<TestRegisteringAttribute>();
        }
    }
}