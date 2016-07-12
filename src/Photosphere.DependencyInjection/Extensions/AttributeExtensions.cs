using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Extensions
{
    public static class AttributeExtensions
    {
        public static IEnumerable<Type> GetMarkedTypes(this Type attributeType)
        {
            Validate(attributeType);
            return attributeType.Assembly.GetAllTypesMarkedByAttribute(attributeType);
        }

        private static void Validate(Type attributeType)
        {
            if (!attributeType.IsAttribute())
            {
                throw new ArgumentException($"`{attributeType.FullName}` is not attribute");
            }
        }
    }
}