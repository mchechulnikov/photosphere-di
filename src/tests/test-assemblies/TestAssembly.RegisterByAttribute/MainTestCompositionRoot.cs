using Photosphere.DependencyInjection;
using TestAssembly.RegisterByAttribute.TestObjects;

namespace TestAssembly.RegisterByAttribute
{
    internal class MainTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.RegisterBy<TestRegisteringAttribute>();
        }
    }
}