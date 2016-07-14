using System;
using System.Linq;

namespace Photosphere.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class CompositionRootAttribute : Attribute
    {
        public CompositionRootAttribute(Type compositionRootType)
        {
            Validate(compositionRootType);
            CompositionRootType = compositionRootType;
        }

        public Type CompositionRootType { get; }

        private static void Validate(Type type)
        {
            if (!type.GetInterfaces().Contains(typeof(ICompositionRoot)))
            {
                throw new ArgumentException($"Provided type `{type.FullName}` not implements {nameof(ICompositionRoot)}");
            }
        }
    }
}