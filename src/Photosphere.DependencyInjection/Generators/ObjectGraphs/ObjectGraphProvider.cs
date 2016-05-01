﻿using System;
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
        public static ObjectGraph Provide(Type type, IRegistry registry, ISet<Type> alreadyProvidedTypes = null)
        {
            alreadyProvidedTypes = MarkTypeAsProcessed(type, alreadyProvidedTypes);
            var registration = GetRegistration(type, registry);
            var constructor = type.GetFirstPublicConstructor();
            var children = GetChildren(alreadyProvidedTypes, constructor, registry);
            return new ObjectGraph(registration, constructor, children);
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

        private static IRegistration GetRegistration(Type type, IRegistry registry)
        {
            CheckForRegistration(type, registry);
            return registry[type];
        }

        private static void CheckForRegistration(Type type, IRegistry registry)
        {
            if (registry.Contains(type))
            {
                return;
            }
            throw new TypeNotRefisteredException(type);
        }

        private static IReadOnlyList<ObjectGraph> GetChildren(
            ISet<Type> alreadyProvidedTypes, ConstructorInfo constructor, IRegistry registry)
        {
            var parametersTypes = GetParametersTypes(constructor);
            if (parametersTypes.IsEmpty())
            {
                return new List<ObjectGraph>();
            }
            CheckForCircleDependency(parametersTypes, alreadyProvidedTypes);
            return parametersTypes.Select(t => Provide(t, registry, alreadyProvidedTypes)).ToList();
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