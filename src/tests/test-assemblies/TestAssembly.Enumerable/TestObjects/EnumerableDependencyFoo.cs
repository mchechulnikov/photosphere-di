using System.Collections.Generic;

namespace TestAssembly.Enumerable.TestObjects
{
    
    internal class EnumerableDependencyFoo : IEnumerableDependencyFoo
    {
        public EnumerableDependencyFoo(IEnumerable<IFoo> services)
        {
            Services = services;
        }

        public IEnumerable<IFoo> Services { get; set; }
    }
}