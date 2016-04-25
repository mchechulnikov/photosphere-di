using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.CilEmitting
{
    internal static class InstantiateMethodGenerator
    {
        public static Func<TTarget> Generate<TTarget>()
        {
            var dynamicMethod = CreateDynamicMethod<TTarget>();
            InstantiateMethodBodyEmitter.GenerateFor<TTarget>(dynamicMethod);
            return CreateDelegate<TTarget>(dynamicMethod);
        }

        private static DynamicMethod CreateDynamicMethod<TTarget>()
        {
            var targetType = typeof(TTarget);
            var methodName = $"CreateInstanceOf{targetType.Name}";
            return new DynamicMethod(methodName, targetType, null, true);
        }

        private static Func<TTarget> CreateDelegate<TTarget>(MethodInfo dynamicMethod)
        {
            return (Func<TTarget>) dynamicMethod.CreateDelegate(typeof(Func<TTarget>));
        }
    }
}