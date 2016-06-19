using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct IfStatementBuilder
    {
        private readonly Label _endBranchLabel;
        private CilEmitter _ilEmitter;
        private ControlFlowDesigner _controlFlowDesigner;

        public IfStatementBuilder(CilEmitter ilEmitter, ControlFlowDesigner controlFlowDesigner, Label endBranchLabel)
        {
            _ilEmitter = ilEmitter;
            _controlFlowDesigner = controlFlowDesigner;
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

        public ControlFlowDesigner BeginBranch()
        {
            _controlFlowDesigner.IntoBranch = true;
            return _controlFlowDesigner;
        }
    }
}