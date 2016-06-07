namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class MixedLifetimesPerRequestDependencies : IMixedLifetimesPerRequestDependencies
    {
        public MixedLifetimesPerRequestDependencies(
            IAlwaysNewFoo alwaysNewFoo,
            IPerRequestFoo perRequestFoo,
            IPerContainerFoo perContainerFoo)
        {
            AlwaysNewFoo = alwaysNewFoo;
            PerRequestFoo = perRequestFoo;
            PerContainerFoo = perContainerFoo;
        }

        public IAlwaysNewFoo AlwaysNewFoo { get; }
        public IPerRequestFoo PerRequestFoo { get; }
        public IPerContainerFoo PerContainerFoo { get; }
    }
}