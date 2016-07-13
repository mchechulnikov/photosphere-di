using System;
using System.Linq;
using System.Reflection;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class MemberInfoExtensions
    {
        public static AttributeUsageAttribute GetAttributeUsage(this MemberInfo attributeType)
        {
            return (AttributeUsageAttribute)attributeType.GetCustomAttributes(typeof(AttributeUsageAttribute)).Single();
        }
    }
}