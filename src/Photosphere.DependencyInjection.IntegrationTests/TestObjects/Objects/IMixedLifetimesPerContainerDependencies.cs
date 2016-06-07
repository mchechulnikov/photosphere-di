namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IMixedLifetimesPerContainerDependencies
    {
        IAlwaysNewFoo AlwaysNewFoo { get; }
        IPerRequestFoo PerRequestFoo { get; }
        IPerContainerFoo PerContainerFoo { get; }
    }
}