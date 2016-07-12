using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots
{
    internal class AssembliesProvider : IAssembliesProvider
    {
        public IEnumerable<IAssemblyWrapper> Provide()
        {
            return
                AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(NotThisAssembly)
                .Select(a => new AssemblyWrapper(a));
        }

        private static bool NotThisAssembly(Assembly a)
        {
            return a != typeof(AssembliesProvider).Assembly;
        }
    }
}