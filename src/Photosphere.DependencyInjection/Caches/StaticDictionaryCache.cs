using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Caches
{
    internal static class StaticDictionaryCache<T1, T2>
    {
        private static readonly IDictionary<T1, T2> Cache = new Dictionary<T1, T2>();

        public static T2 GetOrAdd(T1 t1, Func<T1, T2> getFunc)
        {
            T2 result;
            if (Cache.TryGetValue(t1, out result))
            {
                return result;
            }
            result = getFunc(t1);
            Cache.Add(t1, result);
            return result;
        }
    }
}