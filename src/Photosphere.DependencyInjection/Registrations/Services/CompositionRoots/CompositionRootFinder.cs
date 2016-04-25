using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Registrations.Services.Exceptions;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots
{
    internal class CompositionRootFinder : ICompositionRootFinder
    {
        private readonly IAssembliesProvider _assembliesProvider;

        public CompositionRootFinder(IAssembliesProvider assembliesProvider)
        {
            _assembliesProvider = assembliesProvider;
        }

        public IEnumerable<ICompositionRoot> Find()
        {
            var assemblies = _assembliesProvider.Provide();
            return assemblies.Select(GetCompositionRoot);
        }

        private static ICompositionRoot GetCompositionRoot(Assembly assembly)
        {
            return (ICompositionRoot) GetSingleImplementationTypeOf<ICompositionRoot>(assembly).GetNewInstance();
        }

        private static Type GetSingleImplementationTypeOf<TService>(Assembly assembly)
        {
            var compositionRootTypes = GetCompositionRootTypes<TService>(assembly);
            CheckTypes<TService>(assembly, compositionRootTypes);
            return GetSingle<TService>(assembly, compositionRootTypes);
        }

        private static IList<Type> GetCompositionRootTypes<TService>(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => t.IsImplements<TService>()).ToList();
        }

        private static void CheckTypes<TService>(Assembly assembly, IEnumerable<Type> compositionRootTypes)
        {
            if (compositionRootTypes.IsEmpty())
            {
                throw new CompositionRootNotFoundException(assembly);
            }
        }

        private static Type GetSingle<TService>(Assembly assembly, IEnumerable<Type> compositionRootTypes)
        {
            var result = compositionRootTypes.SingleOrDefault();
            if (result == null)
            {
                throw new SeveralCompositionRootsWasFoundException(assembly);
            }
            return result;
        }
    }
}