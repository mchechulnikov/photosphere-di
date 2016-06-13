using System.Collections.Generic;

namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal class EnumerableDependencyFoo : IEnumerableDependencyFoo
    {
        public EnumerableDependencyFoo(IEnumerable<IService> services)
        {
            Services = services;
        }

        public IEnumerable<IService> Services { get; set; }
    }
}