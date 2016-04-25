using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registration.Services.CompositionRoots
{
    internal class AssembliesProvider : IAssembliesProvider
    {
        public IEnumerable<Assembly> Provide()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}