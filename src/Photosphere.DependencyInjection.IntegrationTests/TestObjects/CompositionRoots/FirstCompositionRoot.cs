using Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects;

namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.CompositionRoots
{
    internal class FirstCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator.Register<IFoo>();
        }
    }
}