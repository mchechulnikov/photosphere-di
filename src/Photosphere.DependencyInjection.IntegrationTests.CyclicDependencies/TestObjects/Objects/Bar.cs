namespace Photosphere.DependencyInjection.IntegrationTests.CyclicDependencies.TestObjects.Objects
{
    internal class Bar : IBar
    {
        private readonly IQiz _qiz;

        public Bar(IQiz qiz)
        {
            _qiz = qiz;
        }
    }
}