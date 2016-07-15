using System.Collections.Generic;

namespace Photosphere.DependencyInjection.TestAssembly.Enumerable.TestObjects
{
    internal interface IReadOnlyCollectionDependencyFoo
    {
        IReadOnlyCollection<IFoo> Services { get; }
    }
}