using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.SystemExtends.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Analysis.Assemblies
{
    internal class AssembliesProvider : IAssembliesProvider
    {
        private readonly IContainerConfiguration _configuration;

        public AssembliesProvider(IContainerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<IAssemblyWrapper> Provide()
        {
            var assemblies = _configuration == null
                ? AppDomain.CurrentDomain.GetAssemblies()
                : _configuration.TargetAssemblies;
            return FilterAndWrap(assemblies);
        }

        private static IEnumerable<IAssemblyWrapper> FilterAndWrap(IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(NotThisAssembly).Select(a => new AssemblyWrapper(a));
        }

        private static bool NotThisAssembly(Assembly a)
        {
            return a != typeof(AssembliesProvider).Assembly;
        }
    }
}