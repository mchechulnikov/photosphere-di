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
    // TODO Rewrite without static
    internal static class ObjectGraphProvider
    {
        public static IObjectGraph Provide(Type implType, IRegistry registry, ISet<Type> alreadyProvidedTypes = null)
        {
            var registration = GetRegistration(implType, registry);
            var constructor = implType.GetFirstPublicConstructor();
            var children = GetChildren(implType, alreadyProvidedTypes, constructor, registry);
            return new ObjectGraph(registration, constructor, children);
        }

        private static IRegistration GetRegistration(Type implType, IRegistry registry)
        {
            CheckForRegistration(implType, registry);
            return registry[implType];
        }

        private static void CheckForRegistration(Type implType, IRegistry registry)
        {
            if (registry.Contains(implType))
            {
                return;
            }
            throw new TypeNotRegisteredException(implType);
        }

        private static IReadOnlyList<IObjectGraph> GetChildren(
            Type implType, ISet<Type> alreadyProvidedTypes, ConstructorInfo constructor, IRegistry registry)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(implType, alreadyProvidedTypes);
            var parametersTypes = GetParametersTypes(constructor);
            if (parametersTypes.IsEmpty())
            {
                return new List<ObjectGraph>();
            }
            CheckForCircleDependency(parametersTypes, alreadyProvidedTypes);
            var result = new List<IObjectGraph>();
            foreach (var type in parametersTypes)
            {
                var graph = Provide(type, registry, alreadyProvidedTypes);
                result.Add(graph);
                alreadyProvidedTypes.Remove(type);
            }
            return result;
        }

        private static ISet<Type> MarkTypeAsProcessed(Type type, ISet<Type> alreadyProvidedTypes)
        {
            if (alreadyProvidedTypes == null)
            {
                alreadyProvidedTypes = new HashSet<Type>();
            }
            alreadyProvidedTypes.Add(type);
            return alreadyProvidedTypes;
        }

        private static IReadOnlyList<Type> GetParametersTypes(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Select(p => p.ParameterType.GetFirstImplementationType()).ToList();
        }

        private static void CheckForCircleDependency(IEnumerable<Type> parametersTypes, ICollection<Type> alreadyProvidedTypes)
        {
            var alreadyProvidedType = parametersTypes.FirstOrDefault(alreadyProvidedTypes.Contains);
            if (alreadyProvidedType != null)
            {
                throw new DetectedCircleDependencyException(alreadyProvidedType);
            }
        }
    }
}