namespace Photosphere.DependencyInjection.IntegrationTests.CyclicDependencies.TestObjects.Objects
{
    internal class Qiz : IQiz
    {
        private readonly IFoo _foo;

        public Qiz(IFoo foo)
        {
            _foo = foo;
        }
    }
}