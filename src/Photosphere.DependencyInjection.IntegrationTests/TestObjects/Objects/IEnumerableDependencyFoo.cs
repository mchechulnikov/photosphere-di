using System.Collections.Generic;

namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IEnumerableDependencyFoo
    {
        IEnumerable<IService> Services { get; }
    }
}