using System;
using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots
{
    internal class AssembliesProvider : IAssembliesProvider
    {
        public IEnumerable<IAssemblyWrapper> Provide()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Select(a => new AssemblyWrapper(a));
        }
    }
}