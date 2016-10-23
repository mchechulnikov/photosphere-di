using System.Collections.Generic;

namespace TestAssembly.Enumerable.TestObjects
{
    internal class ReadOnlyCollectionDependencyFoo : IReadOnlyCollectionDependencyFoo
    {
        public ReadOnlyCollectionDependencyFoo(IReadOnlyCollection<IFoo> services)
        {
            Services = services;
        }

        public IReadOnlyCollection<IFoo> Services { get; set; }
    }
}