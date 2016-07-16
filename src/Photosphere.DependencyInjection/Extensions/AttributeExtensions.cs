using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class AttributeExtensions
    {
        public static IReadOnlyCollection<Type> GetMarkedTypes(this Type attributeType)
        {
            return attributeType.Assembly.GetAllTypesMarkedByAttribute(attributeType).ToHashSet();
        }
    }
}