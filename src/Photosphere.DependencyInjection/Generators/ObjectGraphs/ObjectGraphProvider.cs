using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.Exceptions;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs
{
    internal static class ObjectGraphProvider
    {
        public static ObjectGraph Provide(Type type, ISet<Type> alreadyProvidedTypes = null)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(type, alreadyProvidedTypes);
            var constructor = type.GetFirstPublicConstructor();
            var children = GetChildren(alreadyProvidedTypes, constructor);
            return new ObjectGraph(type, constructor, children);
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

        private static IReadOnlyList<ObjectGraph> GetChildren(ISet<Type> alreadyProvidedTypes, ConstructorInfo constructor)
        {
            var parametersTypes = GetParametersTypes(constructor);
            if (parametersTypes.IsEmpty())
            {
                return new List<ObjectGraph>();
            }
            CheckForCircleDependency(alreadyProvidedTypes, parametersTypes);
            return parametersTypes.Select(t => Provide(t, alreadyProvidedTypes)).ToList();
        }

        private static IReadOnlyList<Type> GetParametersTypes(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Select(p => p.ParameterType.GetFirstImplementationType()).ToList();
        }

        private static void CheckForCircleDependency(ICollection<Type> alreadyProvidedTypes, IEnumerable<Type> parametersTypes)
        {
            var alreadyProvidedType = parametersTypes.FirstOrDefault(alreadyProvidedTypes.Contains);
            if (alreadyProvidedType != null)
            {
                throw new DetectCircleDependencyException(alreadyProvidedType);
            }
        }
    }
}