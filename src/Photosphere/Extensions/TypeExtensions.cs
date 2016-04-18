using System;
using System.Linq;

namespace Photosphere.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsImplements<TInterface>(this Type type)
        {
            var interfaceType = typeof(TInterface);
            return type.IsImplements(interfaceType);
        }

        private static bool IsImplements(this Type type, Type interfaceType)
        {
            return type.GetInterfaces().Any(it => it == interfaceType);
        }

        public static object GetNewInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}