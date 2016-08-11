using System;
using System.Collections.Generic;
using System.Reflection;
using Photosphere.DependencyInjection.Caches;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class AssemblyExtentions
    {
        public static IReadOnlyCollection<Type> ProvideTypes(this Assembly assembly) =>
            StaticDictionaryCache<Assembly, IReadOnlyCollection<Type>>.GetOrAdd(assembly, a => a.GetTypes());
    }
}