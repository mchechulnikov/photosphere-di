using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.StaticServices.Analysis;
using Photosphere.DependencyInjection.StaticServices.DataTransferObjects;

namespace Photosphere.DependencyInjection.StaticServices.CilEmitting
{
    internal class InstantiateMethodBodyEmitter
    {
        private readonly ILGenerator _generator;
        private readonly LocalBuilder _methodResult;
        private readonly IList<LocalBuilder> _localVariables;
        private readonly TypeConstructorInfo _typeConstructorInfo;

        private InstantiateMethodBodyEmitter(ILGenerator generator, Type implementationType)
        {
            _generator = generator;
            _typeConstructorInfo = TypeConstructorInfoProvider.Provide(implementationType);
            _methodResult = _generator.DeclareLocal(implementationType);
            _localVariables = new List<LocalBuilder>();
        }

        public static void GenerateFor<TTarget>(DynamicMethod dynamicMethod)
        {
            var generator = dynamicMethod.GetILGenerator();
            var methodResult = new InstantiateMethodBodyEmitter(generator, typeof(TTarget).GetFirstImplementationType()).Emit();
            GenerateReturnStatement(generator, methodResult);
        }

        private LocalBuilder Emit()
        {
            EmitParameters();
            GenerateInstantiating();
            return _methodResult;
        }

        private void EmitParameters()
        {
            foreach (var parameterType in _typeConstructorInfo.ParametersTypes)
            {
                var localBuilder = new InstantiateMethodBodyEmitter(_generator, parameterType).Emit();
                _localVariables.Add(localBuilder);
            }
        }

        private void GenerateInstantiating()
        {
            GenerateParametersLoading();
            _generator.Emit(OpCodes.Newobj, _typeConstructorInfo.Constructor);
            _generator.Emit(OpCodes.Stloc, _methodResult);
        }

        private void GenerateParametersLoading()
        {
            foreach (var localVariable in _localVariables)
            {
                _generator.Emit(OpCodes.Ldloc, localVariable);
            }
        }

        private static void GenerateReturnStatement(ILGenerator generator, LocalBuilder methodResult)
        {
            generator.Emit(OpCodes.Ldloc, methodResult);
            generator.Emit(OpCodes.Ret);
        }
    }
}