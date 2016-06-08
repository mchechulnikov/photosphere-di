namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class Service22 : IService22
    {
        public Service22(IService11 service11, Service21 service21)
        {
            Service11 = service11;
            Service21 = service21;
        }

        public IService11 Service11 { get; }
        public Service21 Service21 { get; }
    }
}