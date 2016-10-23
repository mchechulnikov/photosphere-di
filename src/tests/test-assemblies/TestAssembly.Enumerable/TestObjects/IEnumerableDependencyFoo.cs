using System.Collections.Generic;

namespace TestAssembly.Enumerable.TestObjects
{
    internal interface IEnumerableDependencyFoo
    {
        IEnumerable<IFoo> Services { get; }
    }
}