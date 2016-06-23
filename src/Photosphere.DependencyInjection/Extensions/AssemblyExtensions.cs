using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Photosphere.DependencyInjection.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetAllDerivedTypesOf(this Assembly assembly, Type serviceType)
        {
            return assembly.GetTypes().Where(serviceType.IsAssignableFrom);
        }
    }
}