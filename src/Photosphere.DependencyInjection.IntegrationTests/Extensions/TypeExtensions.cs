using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.IntegrationTests.Extensions
{
    internal static class TypeExtensions
    {
        public static IEnumerable<FieldInfo> GetPrivateReadonlyFields(this Type type)
        {
            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }
}