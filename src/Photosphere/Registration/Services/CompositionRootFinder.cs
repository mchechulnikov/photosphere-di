using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.Extensions;

namespace Photosphere.Registration.Services
{
    internal class CompositionRootFinder : ICompositionRootFinder
    {
        public IReadOnlyList<ICompositionRoot> Find()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Select(GetCompositionRoot).ToList();
        }

        private static ICompositionRoot GetCompositionRoot(Assembly a)
        {
            return (ICompositionRoot) a.GetTypes().Single(t => t.IsImplements<ICompositionRoot>()).GetNewInstance();
        }
    }
}