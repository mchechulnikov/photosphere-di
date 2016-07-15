using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects.ByAttributes;

namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.CompositionRoots
{
    internal class MainTestCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.RegisterBy<TestRegisteringAttribute>();
        }
    }
}