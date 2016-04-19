using System;
using System.Reflection.Emit;
using Photosphere.CilEmitting.Factories;

namespace Photosphere.CilEmitting.Services
{
    internal class InstantiateMethodGenerator : IInstantiateMethodGenerator
    {
        private readonly IMethodBodyEmitterFactory _methodBodyEmitterFactory;

        public InstantiateMethodGenerator(IMethodBodyEmitterFactory methodBodyEmitterFactory)
        {
            _methodBodyEmitterFactory = methodBodyEmitterFactory;
        }

        public Func<TTarget> Generate<TTarget>()
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

        private void GenerateContent<TTarget>(DynamicMethod dynamicMethod)
        {
            var generator = dynamicMethod.GetILGenerator();
            var methodResult = GetMethodResult<TTarget>(generator);
            GenerateReturnStatement(generator, methodResult);
        }

        private LocalBuilder GetMethodResult<TTarget>(ILGenerator generator)
        {
            return _methodBodyEmitterFactory.Get<TTarget>(generator).Emit();
        }

        private static void GenerateReturnStatement(ILGenerator generator, LocalBuilder methodResult)
        {
            generator.Emit(OpCodes.Ldloc, methodResult);
            generator.Emit(OpCodes.Ret);
        }
    }
}