using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Extensions;

namespace Photosphere.DependencyInjection.CilEmitting
{
    internal class InstantiateMethodBodyEmitter
    {
        private readonly ILGenerator _generator;
        private readonly Type _implementationType;
        private readonly LocalBuilder _methodResult;
        private readonly IList<LocalBuilder> _localVariables;

        private ConstructorInfo ImplementationTypeConstructor => _implementationType.GetFirstPublicConstructor();

        private IEnumerable<Type> ConstructorParametersImplementationTypes
        {
            get
            {
                var parameters = ImplementationTypeConstructor.GetParameters();
                return parameters.Select(p => p.ParameterType.GetFirstImplementationType()).ToList();
            }
        }

        private InstantiateMethodBodyEmitter(ILGenerator generator, Type implementationType)
        {
            _generator = generator;
            _implementationType = implementationType;
            _methodResult = _generator.DeclareLocal(_implementationType);
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
            foreach (var parameterType in ConstructorParametersImplementationTypes)
            {
                var localBuilder = new InstantiateMethodBodyEmitter(_generator, parameterType).Emit();
                _localVariables.Add(localBuilder);
            }
        }

        private void GenerateInstantiating()
        {
            GenerateParametersLoading();
            _generator.Emit(OpCodes.Newobj, ImplementationTypeConstructor);
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