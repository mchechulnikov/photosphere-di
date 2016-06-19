using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs
{
    internal class ObjectGraphProvider : IObjectGraphProvider
    {
        private readonly IRegistry _registry;
        private readonly IGeneratingStrategyProvider _generatingStrategyProvider;

        public ObjectGraphProvider(IRegistry registry, IGeneratingStrategyProvider generatingStrategyProvider)
        {
            _registry = registry;
            _generatingStrategyProvider = generatingStrategyProvider;
        }

        public IObjectGraph Provide(Type serviceType, ISet<Type> alreadyProvidedTypes = null)
        {
            var registration = _registry[serviceType];
            var constructor = registration.DirectImplementationType.GetFirstPublicConstructor();
            var children = registration.IsEnumerable
                ? GetChildrenForEnumerable(serviceType, alreadyProvidedTypes, registration.ImplementationTypes)
                : GetChildren(serviceType, alreadyProvidedTypes, constructor);
            var objectGraph = new ObjectGraph(registration, constructor, children);
            objectGraph.GeneratingStrategy = _generatingStrategyProvider.Provide(objectGraph);
            return objectGraph;
        }

        private IReadOnlyList<IObjectGraph> GetChildrenForEnumerable(Type serviceType, ISet<Type> alreadyProvidedTypes, IReadOnlyCollection<Type> implTypes)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(serviceType, alreadyProvidedTypes);
            return GetChildren(alreadyProvidedTypes, implTypes);
        }

        private IReadOnlyList<IObjectGraph> GetChildren(Type serviceType, ISet<Type> alreadyProvidedTypes, ConstructorInfo constructor)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(serviceType, alreadyProvidedTypes);

            var parametersTypes = GetParametersTypes(constructor);
            if (parametersTypes.IsEmpty())
            {
                return new List<ObjectGraph>();
            }
            return GetChildren(alreadyProvidedTypes, parametersTypes);
        }

        private IReadOnlyList<IObjectGraph> GetChildren(ISet<Type> alreadyProvidedTypes, IReadOnlyCollection<Type> parametersTypes)
        {
            CheckForCircleDependency(parametersTypes, alreadyProvidedTypes);
            var result = new List<IObjectGraph>();
            foreach (var paramServiceType in parametersTypes)
            {
                var graph = Provide(paramServiceType, alreadyProvidedTypes);
                result.Add(graph);
                alreadyProvidedTypes.Remove(paramServiceType);
            }
            return result;
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

        private static IReadOnlyList<Type> GetParametersTypes(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Select(p => p.ParameterType).ToList();
        }

        private static void CheckForCircleDependency(IEnumerable<Type> parametersTypes, ICollection<Type> alreadyProvidedTypes)
        {
            var alreadyProvidedType = parametersTypes.FirstOrDefault(alreadyProvidedTypes.Contains);
            if (alreadyProvidedType != null)
            {
                throw new DetectedCycleDependencyException(alreadyProvidedType);
            }
        }
    }
}