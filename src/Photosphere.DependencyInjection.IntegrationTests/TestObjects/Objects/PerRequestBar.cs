namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class PerRequestBar : IPerRequestBar
    {
        private readonly IPerRequestFoo _foo;

        public PerRequestBar(IPerRequestFoo foo)
        {
            _foo = foo;
        }

        public IPerRequestFoo Foo => _foo;
    }
}