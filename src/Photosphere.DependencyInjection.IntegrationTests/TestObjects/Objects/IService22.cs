namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IService22 : IService2
    {
        IService11 Service11 { get; }
        Service21 Service21 { get; }
    }
}