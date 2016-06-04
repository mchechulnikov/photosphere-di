using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Extensions
{
    internal static class GenericObjectExtensions
    {
        public static IEnumerable<object> GetPrivateReadonlyFieldsObjectsOf<T>(this T obj)
        {
            var fields = typeof(T).GetPrivateReadonlyFields();
            foreach (var field in fields)
            {
                yield return field.GetValue(obj);
            }
        }
    }
}