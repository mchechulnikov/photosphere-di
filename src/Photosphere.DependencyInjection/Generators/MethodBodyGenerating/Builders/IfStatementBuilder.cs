using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct IfStatementBuilder
    {
        private readonly ICilEmitter _ilEmitter;
        private readonly Label _endBranchLabel;
        private ControlFlowBuilder _controlFlowBuilder;

        public IfStatementBuilder(ICilEmitter ilEmitter, ControlFlowBuilder controlFlowBuilder, Label endBranchLabel)
        {
            _ilEmitter = ilEmitter;
            _controlFlowBuilder = controlFlowBuilder;
            _endBranchLabel = endBranchLabel;
        }

        public IfStatementBuilder IsNull(LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Ldnull)
                .Emit(OpCodes.Ceq)
                .Emit(OpCodes.Brfalse, _endBranchLabel);
            return this;
        }

        public ControlFlowBuilder BeginBranch()
        {
            _controlFlowBuilder.IntoBranch = true;
            return _controlFlowBuilder;
        }
    }
}