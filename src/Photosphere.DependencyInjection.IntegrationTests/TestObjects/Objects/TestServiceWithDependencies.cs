namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class TestServiceWithDependencies : ITestServiceWithDependencies
    {
        private readonly IBar _bar;
        private readonly IFoo _foo;

        public TestServiceWithDependencies(IBar bar, IFoo foo)
        {
            _bar = bar;
            _foo = foo;
        }
    }
}