using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes
{
    internal class PerContainerScope : IPerContainerScope
    {
        public PerContainerScope()
        {
            AvailableInstances = new List<object>();
        }

        public IList<object> AvailableInstances { get; private set; }

        public void Add(object instance)
        {
            AvailableInstances.Add(instance);
        }

        public void Dispose()
        {
            AvailableInstances = null;
        }
    }
}