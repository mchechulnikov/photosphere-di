using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class ConstructorInfoExtensions
    {
        public static IReadOnlyList<Type> GetParametersTypes(this ConstructorInfo constructor)
        {
            return constructor.GetParameters().Select(p => p.ParameterType).ToList();
        }
    }
}