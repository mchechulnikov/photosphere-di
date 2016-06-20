using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs
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
            var children = registration.IsEnumerable
                ? GetChildrenForEnumerable(serviceType, alreadyProvidedTypes, registration.ImplementationTypes)
                : GetChildObjectGraphs(serviceType, alreadyProvidedTypes, constructor);
            return new ObjectGraph
            {
                ReturnType = registration.ServiceType,
                ImplementationType = registration.DirectImplementationType,
                Constructor = constructor,
                Children = children,
                GeneratingStrategy = _generatingStrategyProvider.Provide(registration)
            };
        }

        private IReadOnlyList<IObjectGraph> GetChildrenForEnumerable(Type serviceType, ISet<Type> alreadyProvidedTypes, IReadOnlyCollection<Type> implTypes)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(serviceType, alreadyProvidedTypes);
            return GetChildObjectGraphs(alreadyProvidedTypes, implTypes);
        }

        private IReadOnlyList<IObjectGraph> GetChildObjectGraphs(Type serviceType, ISet<Type> alreadyProvidedTypes, ConstructorInfo constructor)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(serviceType, alreadyProvidedTypes);

            var parametersTypes = constructor.GetParametersTypes();
            return GetChildObjectGraphs(alreadyProvidedTypes, parametersTypes);
        }

        private IReadOnlyList<IObjectGraph> GetChildObjectGraphs(ISet<Type> alreadyProvidedTypes, IReadOnlyCollection<Type> parametersTypes)
        {
            if (!parametersTypes.IsEmpty())
            {
                CheckForCircleDependency(parametersTypes, alreadyProvidedTypes);
            }
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