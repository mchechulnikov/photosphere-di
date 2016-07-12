using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetAllTypesMarkedByAttribute(this Assembly assembly, Type attributeType)
        {
            return assembly.GetTypes().Where(t => t.GetCustomAttributes(attributeType).Any());
        }

        public static IEnumerable<Type> GetAllDerivedTypesOf(this Assembly assembly, Type serviceType)
        {
            return serviceType.IsGenericType
                ? AllDerivedTypesOfGeneric(assembly, serviceType)
                : assembly.GetTypes().Where(serviceType.IsAssignableFrom);
        }

        private static IEnumerable<Type> AllDerivedTypesOfGeneric(Assembly assembly, Type serviceType)
        {
            var result = new List<Type>();

            var genericDerivedTypes = assembly.GetTypes().Where(serviceType.IsAssignableFromGenericType).ToHashSet();
            result.AddRange(genericDerivedTypes);

            var collection = genericDerivedTypes
                .Select(gdt => serviceType.IsInterface
                    ? gdt.GetInterface(serviceType.Name)
                    : gdt.GetBaseType(serviceType.Name))
                .Where(gbt => gbt != null && gbt.IsConstructedGenericType);
            result.AddRange(collection);

            return result;
        }
    }
}