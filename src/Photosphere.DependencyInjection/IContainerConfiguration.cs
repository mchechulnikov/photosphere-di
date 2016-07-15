using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection
{
    public interface IContainerConfiguration
    {
        IEnumerable<Assembly> TargetAssemblies { get; }
    }
}