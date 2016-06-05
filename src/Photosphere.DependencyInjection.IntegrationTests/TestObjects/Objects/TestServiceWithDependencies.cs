namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class TestServiceWithDependencies : ITestServiceWithDependencies
    {
        private readonly IFoo _foo;
        private readonly IBar _bar;

        public TestServiceWithDependencies(IFoo foo, IBar bar)
        {
            _bar = bar;
            _foo = foo;
        }

        public IFoo Foo => _foo;
        public IBar Bar => _bar;
    }
}