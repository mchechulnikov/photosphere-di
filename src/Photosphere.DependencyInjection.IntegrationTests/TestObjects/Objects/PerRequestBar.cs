namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class PerRequestBar : IPerRequestBar
    {
        public PerRequestBar(IPerRequestFoo perRequestFoo)
        {
            PerRequestFoo = perRequestFoo;
        }

        public IPerRequestFoo PerRequestFoo { get; }
    }
}