using System;
using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Registrations.Services.Exceptions;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots
{
    internal class CompositionRootProvider : ICompositionRootProvider
    {
        private readonly IAssembliesProvider _assembliesProvider;

        public CompositionRootProvider()
        {
            _assembliesProvider = new AssembliesProvider();
        }

        public IEnumerable<ICompositionRoot> Provide()
        {
            var assemblies = _assembliesProvider.Provide().ToList();
            return assemblies.Select(GetCompositionRoot);
        }

        private static ICompositionRoot GetCompositionRoot(IAssemblyWrapper assembly)
        {
            return (ICompositionRoot) GetSingleImplementationTypeOf<ICompositionRoot>(assembly).GetNewInstance();
        }

        private static Type GetSingleImplementationTypeOf<TService>(IAssemblyWrapper assembly)
        {
            var compositionRootTypes = GetCompositionRootTypes<TService>(assembly);
            CheckTypes<TService>(assembly, compositionRootTypes.ToList());
            return compositionRootTypes.First();
        }

        private static IList<Type> GetCompositionRootTypes<TService>(IAssemblyWrapper assembly)
        {
            return assembly.Types.Where(t => t.IsImplements<TService>()).ToList();
        }

        private static void CheckTypes<TService>(IAssemblyWrapper assembly, IReadOnlyList<Type> compositionRootTypes)
        {
            if (compositionRootTypes.IsEmpty())
            {
                throw new CompositionRootNotFoundException(assembly);
            }
            if (compositionRootTypes.Count() > 1)
            {
                throw new SeveralCompositionRootsWasFoundException(assembly, compositionRootTypes);
            }
        }
    }
}