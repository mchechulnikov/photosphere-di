namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IAlwaysNewBar
    {
        IAlwaysNewFoo AlwaysNewFoo { get; }
        IPerRequestBar PerRequestBar { get; }
    }
}