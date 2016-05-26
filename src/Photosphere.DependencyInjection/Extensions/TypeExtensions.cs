using System;
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

        public static bool IsImplements(this Type type, Type interfaceType)
        {
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"Type `{interfaceType.Name}` must be interface");
            }
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
            return assemblies.Select(a => a.GetFirstOrDefaultImplementationTypeOf(type)).First(t => t != null);
        }

        public static ConstructorInfo GetFirstPublicConstructor(this Type type)
        {
            return
                type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default).FirstOrDefault()
                ?? type.GetConstructor(Type.EmptyTypes);
        }
    }
}