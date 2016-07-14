using System;
using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.Attributes;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots.Exceptions;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots.ServiceCompositionRoots;
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
            var compositionRootType = GetImplementationTypeOfCompositionRoot(assembly);
            if (compositionRootType != null)
            {
                return (ICompositionRoot) compositionRootType.GetNewInstance();
            }

            var serviceTypes = assembly.GetAttributes<RegisterDependenciesAttribute>().Select(a => a.ServiceType).ToList();
            var targetAttributesTypes = assembly.GetAttributes<RegisterDependenciesByAttribute>().Select(a => a.RegistrationAttributeType).ToList();

            if (serviceTypes.IsEmpty() && targetAttributesTypes.IsEmpty())
            {
                return null;
            }

            return new DefaultCompositionRoot(serviceTypes, targetAttributesTypes, assembly.Assembly);
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