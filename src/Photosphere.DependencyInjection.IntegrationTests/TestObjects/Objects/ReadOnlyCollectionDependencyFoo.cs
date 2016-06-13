using System.Collections.Generic;

namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class ReadOnlyCollectionDependencyFoo : IReadOnlyCollectionDependencyFoo
    {
        public ReadOnlyCollectionDependencyFoo(IReadOnlyCollection<IService> services)
        {
            Services = services;
        }

        public IReadOnlyCollection<IService> Services { get; set; }
    }
}