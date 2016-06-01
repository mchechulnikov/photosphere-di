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

        public CompositionRootProvider(IAssembliesProvider assembliesProvider)
        {
            _assembliesProvider = assembliesProvider;
        }

        public IEnumerable<ICompositionRoot> Provide()
        {
            var assemblies = _assembliesProvider.Provide().ToList();
            return assemblies.Select(GetCompositionRoot).NotNull();
        }

        private static ICompositionRoot GetCompositionRoot(IAssemblyWrapper assembly)
        {
            return (ICompositionRoot) GetSingleImplementationTypeOf<ICompositionRoot>(assembly)?.GetNewInstance();
        }

        private static Type GetSingleImplementationTypeOf<TService>(IAssemblyWrapper assembly)
        {
            var compositionRootTypes = GetCompositionRootTypes<TService>(assembly);
            CheckTypes(assembly, compositionRootTypes.ToList());
            return compositionRootTypes.FirstOrDefault();
        }

        private static IList<Type> GetCompositionRootTypes<TService>(IAssemblyWrapper assembly)
        {
            return assembly.Types.Where(t => t.IsImplements<TService>()).ToList();
        }

        private static void CheckTypes(IAssemblyWrapper assembly, IReadOnlyList<Type> compositionRootTypes)
        {
            if (compositionRootTypes.Count() > 1)
            {
                throw new SeveralCompositionRootsWasFoundException(assembly, compositionRootTypes);
            }
        }
    }
}