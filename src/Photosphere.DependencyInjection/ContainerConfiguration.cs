using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection
{
    public class ContainerConfiguration : IContainerConfiguration
    {
        public ContainerConfiguration(IEnumerable<Assembly> targetAssemblies)
        {
            TargetAssemblies = targetAssemblies;
        }

        public IEnumerable<Assembly> TargetAssemblies { get; set; }
    }
}