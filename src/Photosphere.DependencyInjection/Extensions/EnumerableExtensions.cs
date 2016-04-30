using System.Collections.Generic;
using System.Linq;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.IsEmpty<T>();
        }
    }
}