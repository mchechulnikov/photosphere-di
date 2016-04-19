using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Photosphere.CilEmitting.Services
{
    internal class InstantiateMethodBodyEmitter : IInstantiateMethodBodyEmitter
    {
        private readonly ILGenerator _generator;
        private readonly Type _targetType;
        private readonly LocalBuilder _methodResult;
        private readonly IList<LocalBuilder> _localVariables;

        private ConstructorInfo TargetTypeConstructor => _targetType.GetConstructors().First();

        public InstantiateMethodBodyEmitter(ILGenerator generator, Type targetType)
        {
            _generator = generator;
            _targetType = targetType;
            _methodResult = _generator.DeclareLocal(_targetType);
            _localVariables = new List<LocalBuilder>();
        }

        public LocalBuilder Emit()
        {
            EmitParameters();
            GenerateInstantiating();
            return _methodResult;
        }

        private void EmitParameters()
        {
            var ctorParameterTypes = TargetTypeConstructor.GetParameters().Select(p => p.ParameterType);
            foreach (var parameterType in ctorParameterTypes)
            {
                var localBuilder = new InstantiateMethodBodyEmitter(_generator, parameterType).Emit();
                _localVariables.Add(localBuilder);
            }
        }

        private void GenerateInstantiating()
        {
            GenerateParametersLoading();
            _generator.Emit(OpCodes.Newobj, TargetTypeConstructor);
            _generator.Emit(OpCodes.Stloc, _methodResult);
        }

        private void GenerateParametersLoading()
        {
            foreach (var localVariable in _localVariables)
            {
                _generator.Emit(OpCodes.Ldloc, localVariable);
            }
        }
    }
}