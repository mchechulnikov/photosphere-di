using System.Collections.Generic;

namespace TestAssembly.Enumerable.TestObjects
{
    internal interface IReadOnlyCollectionDependencyFoo
    {
        IReadOnlyCollection<IFoo> Services { get; }
    }
}