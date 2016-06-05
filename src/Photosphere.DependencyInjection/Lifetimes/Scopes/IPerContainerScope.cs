using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Lifetimes.Scopes
{
    internal interface IPerContainerScope : IScope
    {
        void Add(object instance);

        IList<object> AvailableInstances { get; }
    }
}