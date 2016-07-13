using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class TypeExtensions
    {
        public static Type GetBaseType(this Type type, string name)
        {
            while (true)
            {
                var baseType = type.BaseType;
                if (baseType == typeof(object) || baseType == null)
                {
                    return null;
                }
                if (baseType.Name == name)
                {
                    return baseType;
                }
                type = baseType;
            }
        }

        public static bool IsInstantiatible(this Type type)
        {
            return !type.IsAbstract && !type.IsInterface;
        }

        public static bool IsInstantiatibleUserDefinedClass(this Type type)
        {
            return
                !type.IsAbstract
                && !type.IsInterface
                && !type.IsGenericType;
        }

        public static bool IsImplements<TInterface>(this Type type)
        {
            var interfaceType = typeof(TInterface);
            return type.IsImplements(interfaceType);
        }

        public static object GetNewInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static IEnumerable<Type> GetAllDerivedTypes(this Type type)
        {
            return type.Assembly.GetAllDerivedTypesOf(type).Where(t => t != null);
        }

        public static ConstructorInfo GetFirstPublicConstructor(this Type type)
        {
            return
                type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default).FirstOrDefault()
                ?? type.GetConstructor(Type.EmptyTypes);
        }

        public static Type MakeGenericWrappedBy(this Type type, Type genericType)
        {
            return genericType.MakeGenericType(type);
        }

        public static string GetFormattedName(this Type type)
        {
            return type.IsGenericType
                ? type.Name + "`" + String.Join("`", type.GenericTypeArguments.Select(x => x.Name).ToArray())
                : type.Name;
        }

        public static bool IsAssignableFromGenericType(this Type genericType, Type givenType)
        {
            if (givenType == null || genericType == null)
            {
                return false;
            }

            return givenType == genericType
              || givenType.MapsToGenericTypeDefinition(genericType)
              || givenType.HasInterfaceThatMapsToGenericTypeDefinition(genericType)
              || genericType.IsAssignableFromGenericType(givenType.BaseType);
        }

        public static bool IsAttribute(this Type type)
        {
            while (true)
            {
                var baseType = type.BaseType;
                if (baseType == typeof(object) || baseType == null)
                {
                    return false;
                }
                if (baseType == typeof(Attribute))
                {
                    return true;
                }
                type = baseType;
            }
        }

        private static bool IsImplements(this Type type, Type serviceType)
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

        private static bool HasInterfaceThatMapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return givenType
              .GetInterfaces()
              .Where(it => it.IsGenericType)
              .Any(it => it.GetGenericTypeDefinition() == genericType);
        }

        private static bool MapsToGenericTypeDefinition(this Type givenType, Type genericType)
        {
            return genericType.IsGenericTypeDefinition
              && givenType.IsGenericType
              && givenType.GetGenericTypeDefinition() == genericType;
        }
    }
}