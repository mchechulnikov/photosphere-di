namespace Photosphere.DependencyInjection.TestAssembly.Lifetimes.TestObjects
{
    internal interface IMixedLifetimesPerContainerDependencies
    {
        IAlwaysNewFoo AlwaysNewFoo { get; }
        IPerRequestFoo PerRequestFoo { get; }
        IPerContainerFoo PerContainerFoo { get; }
    }
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

    internal interface IMixedLifetimesPerRequestDependencies
    {
        IAlwaysNewFoo AlwaysNewFoo { get; }
        IPerRequestFoo PerRequestFoo { get; }
        IPerContainerFoo PerContainerFoo { get; }
    }
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