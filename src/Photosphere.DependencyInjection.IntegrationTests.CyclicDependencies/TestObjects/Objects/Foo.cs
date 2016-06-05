namespace Photosphere.DependencyInjection.IntegrationTests.CyclicDependencies.TestObjects.Objects
{
    internal class Foo : IFoo
    {
        private readonly IBar _bar;

        public Foo(IBar bar)
        {
            _bar = bar;
        }
    }
}