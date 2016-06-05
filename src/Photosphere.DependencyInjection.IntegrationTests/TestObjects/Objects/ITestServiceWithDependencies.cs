namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface ITestServiceWithDependencies
    {
        IPerRequestFoo PerRequestFoo { get; }
        IPerRequestBar PerRequestBar { get; }
        IAlwaysNewFoo AlwaysNewFoo { get; }
        IAlwaysNewBar AlwaysNewBar { get; }
    }
}