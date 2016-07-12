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

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Where(x => x != null);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
            return new HashSet<T>(enumerable);
        }

        public static bool HasSeveralElements<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() > 1;
        }

        public static bool HasNonSeveralElements<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.HasSeveralElements();
        }
    }
}