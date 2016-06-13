using System.Collections.Generic;

namespace Photosphere.DependencyInjection.IntegrationTests.TestObjects.Objects
{
    internal interface IReadOnlyCollectionDependencyFoo
    {
        IReadOnlyCollection<IService> Services { get; }
    }
}