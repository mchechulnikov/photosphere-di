using System;
using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Services;
using Photosphere.DependencyInjection.Generation.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Generation.ObjectGraphs.Exceptions;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generation.ObjectGraphs
{
    internal class ObjectGraphProvider : IObjectGraphProvider
    {
        private readonly IRegistry _registry;
        private readonly IGeneratingStrategyProvider _generatingStrategyProvider;

        public ObjectGraphProvider(
            IRegistry registry,
            IGeneratingStrategyProvider generatingStrategyProvider)
        {
            _registry = registry;
            _generatingStrategyProvider = generatingStrategyProvider;
        }

        public IObjectGraph Provide(Type serviceType, ISet<Type> alreadyProvidedTypes = null)
        {
            var registration = _registry[serviceType];
            var constructor = registration.DirectImplementationType.GetFirstPublicConstructor();
            var childTypes = registration.IsEnumerable ? registration.ImplementationTypes : constructor.GetParametersTypes();
            return new ObjectGraph
            {
                ReturnType = registration.ServiceType,
                ImplementationType = registration.DirectImplementationType,
                Constructor = constructor,
                Children = GetChildObjectGraphs(serviceType, alreadyProvidedTypes, childTypes),
                GeneratingStrategy = _generatingStrategyProvider.Provide(registration)
            };
        }

        private IReadOnlyList<IObjectGraph> GetChildObjectGraphs(Type serviceType, ISet<Type> alreadyProvidedTypes, IReadOnlyCollection<Type> childTypes)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(serviceType, alreadyProvidedTypes);
            CheckForCircleDependency(childTypes, alreadyProvidedTypes);
            return GetChildObjectGraphs(childTypes, alreadyProvidedTypes);
        }

        private static ISet<Type> MarkTypeAsProcessed(Type serviceType, ISet<Type> alreadyProvidedTypes)
        {
            if (alreadyProvidedTypes == null)
            {
                alreadyProvidedTypes = new HashSet<Type>();
            }
            alreadyProvidedTypes.Add(serviceType);
            return alreadyProvidedTypes;
        }

        private static void CheckForCircleDependency(IReadOnlyCollection<Type> childTypes, ICollection<Type> alreadyProvidedTypes)
        {
            if (childTypes.IsEmpty())
            {
                return;
            }
            var alreadyProvidedType = childTypes.FirstOrDefault(alreadyProvidedTypes.Contains);
            if (alreadyProvidedType != null)
            {
                throw new DetectedCycleDependencyException(alreadyProvidedType);
            }
        }

        private IReadOnlyList<IObjectGraph> GetChildObjectGraphs(IEnumerable<Type> childTypes, ISet<Type> alreadyProvidedTypes)
        {
            var result = new List<IObjectGraph>();
            foreach (var type in childTypes)
            {
                var graph = Provide(type, alreadyProvidedTypes);
                alreadyProvidedTypes.Remove(type);
                result.Add(graph);
            }
            return result;
        }
    }
}