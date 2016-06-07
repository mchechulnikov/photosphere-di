namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IMixedLifetimesPerRequestDependencies
    {
        IAlwaysNewFoo AlwaysNewFoo { get; }
        IPerRequestFoo PerRequestFoo { get; }
        IPerContainerFoo PerContainerFoo { get; }
    }
}