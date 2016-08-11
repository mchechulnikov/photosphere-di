using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;

namespace Photosphere.DependencyInjection.Types
{
    internal class TypesProvider : ITypesProvider
    {
        public IReadOnlyCollection<Type> GetAllDerivedTypesFrom(Type type, Assembly assembly)
        {
            var allDerivedTypesOf = GetAllDerivedTypesOf(assembly, type).ToHashSet();
            var allDerivedTypeOf2 = GetAllDerivedTypesOf(type.Assembly, type).ToHashSet();
            return allDerivedTypesOf.Where(t => t != null).Union(allDerivedTypeOf2).ToHashSet();
        }

        public IReadOnlyCollection<Type> GetMarkedTypes(Type attributeType)
        {
            Contract.Assert(attributeType.IsAttribute());
            return attributeType.Assembly.ProvideTypes().Where(t => t.GetCustomAttributes(attributeType).Any()).ToHashSet();
        }

        private static IEnumerable<Type> GetAllDerivedTypesOf(Assembly assembly, Type serviceType)
        {
            return serviceType.IsGenericType
                ? AllDerivedTypesOfGeneric(assembly, serviceType)
                : assembly.ProvideTypes().Where(serviceType.IsAssignableFrom);
        }

        private static IEnumerable<Type> AllDerivedTypesOfGeneric(Assembly assembly, Type serviceType)
        {
            var result = new List<Type>();

            var genericDerivedTypes =
                assembly.ProvideTypes().Where(serviceType.IsAssignableFromGenericType).ToHashSet();
            result.AddRange(genericDerivedTypes);

            var collection = genericDerivedTypes
                .Select(gdt => serviceType.IsInterface ? gdt.GetInterface(serviceType.Name) : gdt.GetBaseType(serviceType.Name))
                .Where(gbt => gbt != null && gbt.IsConstructedGenericType);
            result.AddRange(collection);

            return result;
        }
    }
}