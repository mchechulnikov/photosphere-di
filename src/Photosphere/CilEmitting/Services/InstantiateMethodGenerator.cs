using System;
using System.Reflection.Emit;

namespace Photosphere.CilEmitting.Services
{
    internal class InstantiateMethodGenerator : IInstantiateMethodGenerator
    {
        public Func<TTarget> Generate<TTarget>()
        {
            var targetType = typeof(TTarget);
            var dynamicMethod = CreateDynamicMethod(targetType);
            GenerateBody(dynamicMethod, targetType);
            return (Func<TTarget>)dynamicMethod.CreateDelegate(typeof(Func<TTarget>));
        }

        private static DynamicMethod CreateDynamicMethod(Type targetType)
        {
            var methodName = $"CreateInstanceOf{targetType.Name}";
            return new DynamicMethod(methodName, targetType, null);
        }

        private static void GenerateBody(DynamicMethod dynamicMethod, Type targetType)
        {
            var generator = dynamicMethod.GetILGenerator();
            var methodResult = new InstantiateMethodBodyEmitter(generator, targetType).Emit();
            GenerateReturnStatement(generator, methodResult);
        }

        private static void GenerateReturnStatement(ILGenerator generator, LocalBuilder methodResult)
        {
            generator.Emit(OpCodes.Ldloc, methodResult);
            generator.Emit(OpCodes.Ret);
        }
    }
}