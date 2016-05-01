namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects
{
    internal class FirstCompositionRoot : ICompositionRoot
    {
        public void Compose(IRegistrator registrator)
        {
            registrator
                .Register<IFoo>()
                .Register<IBar>()
                .Register<IQiz>();
        }
    }
}