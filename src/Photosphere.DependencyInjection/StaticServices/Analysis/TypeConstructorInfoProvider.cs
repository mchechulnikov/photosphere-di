using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.StaticServices.DataTransferObjects;

namespace Photosphere.DependencyInjection.StaticServices.Analysis
{
    internal static class TypeConstructorInfoProvider
    {
        public static TypeConstructorInfo Provide(Type type)
        {
            var constructor = type.GetFirstPublicConstructor();
            return new TypeConstructorInfo
            {
                Type = type,
                Constructor = constructor,
                ParametersTypes = GetParametersTypes(constructor)
            };
        }

        private static List<Type> GetParametersTypes(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Select(p => p.ParameterType.GetFirstImplementationType()).ToList();
        }
    }
}