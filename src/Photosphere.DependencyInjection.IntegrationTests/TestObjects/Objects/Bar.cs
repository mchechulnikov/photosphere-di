namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class Bar : IBar
    {
        private readonly IFoo _foo;

        public Bar(IFoo foo)
        {
            _foo = foo;
        }

        public IFoo Foo => _foo;
    }
}