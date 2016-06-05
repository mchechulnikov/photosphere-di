using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs
{
    internal class ObjectGraphProvider : IObjectGraphProvider
    {
        public IObjectGraph Provide(
            Type serviceType, Type implType, IRegistry registry, ISet<Type> alreadyProvidedTypes = null)
        {
            var registration = GetRegistration(serviceType, registry);
            var constructor = implType.GetFirstPublicConstructor();
            var children = GetChildren(serviceType, alreadyProvidedTypes, constructor, registry);
            return new ObjectGraph(registration, constructor, children);
        }

        private static IRegistration GetRegistration(Type serviceType, IRegistry registry)
        {
            CheckForRegistration(serviceType, registry);
            return registry[serviceType];
        }

        private static void CheckForRegistration(Type serviceType, IRegistry registry)
        {
            if (registry.Contains(serviceType))
            {
                return;
            }
            throw new TypeNotRegisteredException(serviceType);
        }

        private IReadOnlyList<IObjectGraph> GetChildren(
            Type serviceType, ISet<Type> alreadyProvidedTypes, ConstructorInfo constructor, IRegistry registry)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(serviceType, alreadyProvidedTypes);
            var parametersTypes = GetParametersTypes(constructor);
            if (parametersTypes.IsEmpty())
            {
                return new List<ObjectGraph>();
            }
            CheckForCircleDependency(parametersTypes, alreadyProvidedTypes);
            var result = new List<IObjectGraph>();
            foreach (var paramServiceType in parametersTypes)
            {
                var paramImplType = paramServiceType.GetFirstImplementationType();
                var graph = Provide(paramServiceType, paramImplType, registry, alreadyProvidedTypes);
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