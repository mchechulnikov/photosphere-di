using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsImplements<TInterface>(this Type type)
        {
            var interfaceType = typeof(TInterface);
            return type.IsImplements(interfaceType);
        }

        public static bool IsImplements(this Type type, Type serviceType)
        {
            if (!serviceType.IsInterface)
            {
                throw new ArgumentException($"Type `{serviceType.Name}` must be interface");
            }
            return
                type.GetInterfaces().Any(it => it == serviceType)
                && !type.IsAbstract
                && !type.IsInterface;
        }

        public static object GetNewInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static IEnumerable<Type> GetAllDerivedTypes(this Type type)
        {
            return type.Assembly.GetAllDerivedTypesOf(type).Where(t => t != null);
        }

        public static Type GetFirstImplementationType(this Type type)
        {
            if (!type.IsAbstract && !type.IsInterface)
            {
                return type;
            }
            return type.Assembly.GetFirstOrDefaultImplementationTypeOf(type);
        }

        public static ConstructorInfo GetFirstPublicConstructor(this Type type)
        {
            return
                type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default).FirstOrDefault()
                ?? type.GetConstructor(Type.EmptyTypes);
        }

        public static IEnumerable<FieldInfo> GetPrivateReadonlyFields(this Type type)
        {
            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}