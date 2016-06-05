namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class TestServiceWithDependencies : ITestServiceWithDependencies
    {

        public TestServiceWithDependencies(IPerRequestFoo perRequestFoo, IPerRequestBar perRequestBar, IAlwaysNewFoo alwaysNewFoo, IAlwaysNewBar alwaysNewBar)
        {
            PerRequestFoo = perRequestFoo;
            PerRequestBar = perRequestBar;
            AlwaysNewFoo = alwaysNewFoo;
            AlwaysNewBar = alwaysNewBar;
        }

        public IPerRequestFoo PerRequestFoo { get; }
        public IPerRequestBar PerRequestBar { get; }
        public IAlwaysNewFoo AlwaysNewFoo { get; }
        public IAlwaysNewBar AlwaysNewBar { get; }
    }
}