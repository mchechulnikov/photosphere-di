using System;
using System.Linq;
using System.Reflection;

namespace Photosphere.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsImplements<TInterface>(this Type type)
        {
            var interfaceType = typeof(TInterface);
            return type.IsImplements(interfaceType);
        }

        public static bool IsImplements(this Type type, Type interfaceType)
        {
            return type.GetInterfaces().Any(it => it == interfaceType);
        }

        public static object GetNewInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static Type GetFirstImplementationType(this Type type)
        {
            if (!type.IsAbstract && !type.IsInterface)
            {
                return type;
            }
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Select(a => a.GetFirstImplementationTypeOf(type)).First();
        }

        public static ConstructorInfo GetFirstConstructor(this Type type)
        {
            return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).First();
        }
    }
}