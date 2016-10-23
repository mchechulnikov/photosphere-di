using System.Collections.Generic;
using System.Linq;

namespace Photosphere.DependencyInjection.UnitTests.TestUtils.Extensions
{
    internal static class EnumerableExtensions
    {
        public static void IdleEnumerate<T>(this IEnumerable<T> enumerable)
        {
            enumerable.Count();
        }
    }
}