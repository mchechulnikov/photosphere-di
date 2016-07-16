using System;
using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.Attributes;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Initialization.Analysis.Assemblies;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots.Exceptions;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots.ServiceCompositionRoots;
using Photosphere.DependencyInjection.SystemExtends.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots
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
            var compositionRootType = GetImplementationTypeOfCompositionRoot(assembly);
            if (compositionRootType != null)
            {
                return (ICompositionRoot) compositionRootType.GetNewInstance();
            }

            var registerDependenciesAttributes = assembly.GetAttributes<RegisterDependenciesAttribute>();
            var registerDependenciesByAttributes = assembly.GetAttributes<RegisterDependenciesByAttribute>();

            if (registerDependenciesAttributes.IsEmpty() && registerDependenciesByAttributes.IsEmpty())
            {
                return null;
            }

            return new DefaultCompositionRoot(registerDependenciesAttributes, registerDependenciesByAttributes, assembly.Assembly);
        }

        private static Type GetImplementationTypeOfCompositionRoot(IAssemblyWrapper assembly)
        {
            var compositionRootAttributes = assembly.GetAttributes<CompositionRootAttribute>();
            if (compositionRootAttributes.Any())
            {
                return compositionRootAttributes.Single().CompositionRootType;
            }

            var compositionRootTypes = SearchCompositionRootTypes(assembly);
            if (compositionRootTypes.HasSeveralElements())
            {
                throw new SeveralCompositionRootsWasFoundException(assembly, compositionRootTypes);
            }
            return compositionRootTypes.SingleOrDefault();
        }

        private static IReadOnlyCollection<Type> SearchCompositionRootTypes(IAssemblyWrapper assembly)
        {
            return assembly.Types.Where(t => t.IsImplements<ICompositionRoot>()).ToList();
        }
    }
}