using System;
using System.Collections.Generic;
using System.Linq;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class EnumerableOfTypeExtensions
    {
        public static IEnumerable<Type> GetTypesThatImplements(this IEnumerable<Type> filteredTypes, Type type)
        {
            return filteredTypes.Where(t => t.IsInstantiatibleUserDefinedClass() && type.IsAssignableFrom(t));
        }
    }
}