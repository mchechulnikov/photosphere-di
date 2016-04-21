using System;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.CilEmitting
{
    internal static class InstantiateMethodGenerator
    {
        public static Func<TTarget> Generate<TTarget>()
        {
            var dynamicMethod = CreateDynamicMethod<TTarget>();
            GenerateContent<TTarget>(dynamicMethod);
            return (Func<TTarget>)dynamicMethod.CreateDelegate(typeof(Func<TTarget>));
        }

        private static DynamicMethod CreateDynamicMethod<TTarget>()
        {
            var targetType = typeof(TTarget);
            var methodName = $"CreateInstanceOf{targetType.Name}";
            return new DynamicMethod(methodName, targetType, null);
        }

        private static void GenerateContent<TTarget>(DynamicMethod dynamicMethod)
        {
            var generator = dynamicMethod.GetILGenerator();
            var methodResult = new InstantiateMethodBodyEmitter(generator, typeof(TTarget)).Emit();
            GenerateReturnStatement(generator, methodResult);
        }

        private static void GenerateReturnStatement(ILGenerator generator, LocalBuilder methodResult)
        {
            generator.Emit(OpCodes.Ldloc, methodResult);
            generator.Emit(OpCodes.Ret);
        }
    }
}