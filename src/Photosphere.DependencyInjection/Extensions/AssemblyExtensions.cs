using System;
using System.Linq;
using System.Reflection;

namespace Photosphere.Extensions
{
    public static class AssemblyExtensions
    {
        public static Type GetSingleImplementationTypeOf<TService>(this Assembly a)
        {
            return a.GetTypes().Single(t => t.IsImplements<TService>());
        }

        public static Type GetFirstImplementationTypeOf(this Assembly a, Type serviceType)
        {
            return a.GetTypes().First(t => t.IsImplements(serviceType));
        }
    }
}