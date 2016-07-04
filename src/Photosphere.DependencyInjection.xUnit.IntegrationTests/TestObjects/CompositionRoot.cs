namespace Photosphere.DependencyInjection.xUnit.IntegrationTests.TestObjects
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IFoo>()
                .Register<IBar>();
        }
    }
}