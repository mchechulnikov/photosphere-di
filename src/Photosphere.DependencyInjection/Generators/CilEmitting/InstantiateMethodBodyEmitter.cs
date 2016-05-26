using System.Collections.Generic;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Generators.CilEmitting
{
    // TODO Rewrite without static
    internal class InstantiateMethodBodyEmitter
    {
        private readonly ILGenerator _generator;
        private readonly LocalBuilder _methodResult;
        private readonly IList<LocalBuilder> _localVariables;
        private readonly IObjectGraph _objectGraph;

        public static void GenerateFor<TTarget>(ILGenerator ilGenerator, IObjectGraph objectGraph)
        {
            var methodResult = new InstantiateMethodBodyEmitter(ilGenerator, objectGraph).Emit();
            GenerateReturnStatement(ilGenerator, methodResult);
        }

        private InstantiateMethodBodyEmitter(ILGenerator generator, IObjectGraph objectGraph)
        {
            _generator = generator;
            _objectGraph = objectGraph;
            _methodResult = _generator.DeclareLocal(_objectGraph.ImplementationType);
            _localVariables = new List<LocalBuilder>();
        }

        private LocalBuilder Emit()
        {
            EmitParameters();
            GenerateInstantiating();
            return _methodResult;
        }

        private void EmitParameters()
        {
            foreach (var childObjectGraph in _objectGraph.Children)
            {
                var localBuilder = new InstantiateMethodBodyEmitter(_generator, childObjectGraph).Emit();
                _localVariables.Add(localBuilder);
            }
        }

        private void GenerateInstantiating()
        {
            GenerateParametersLoading();
            if (_objectGraph.RegisteredInstance == null)
            {
                _generator.Emit(OpCodes.Newobj, _objectGraph.Constructor);
            }
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