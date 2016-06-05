namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class AlwaysNewBar : IAlwaysNewBar
    {
        public AlwaysNewBar(IAlwaysNewFoo alwaysNewFoo, IPerRequestBar perRequestBar)
        {
            AlwaysNewFoo = alwaysNewFoo;
            PerRequestBar = perRequestBar;
        }

        public IAlwaysNewFoo AlwaysNewFoo { get; }
        public IPerRequestBar PerRequestBar { get; }
    }
}