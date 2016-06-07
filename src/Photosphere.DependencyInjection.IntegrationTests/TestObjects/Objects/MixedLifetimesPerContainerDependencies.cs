namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class MixedLifetimesPerContainerDependencies : IMixedLifetimesPerContainerDependencies
    {
        public MixedLifetimesPerContainerDependencies(
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