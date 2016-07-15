using System.Collections.Generic;

namespace Photosphere.DependencyInjection.TestAssembly.Enumerable.TestObjects
{
    internal interface IEnumerableDependencyFoo
    {
        IEnumerable<IFoo> Services { get; }
    }
}