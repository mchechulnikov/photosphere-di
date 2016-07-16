using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Designers
{
    internal struct IfStatementDesigner
    {
        private readonly Label _endBranchLabel;
        private CilEmitter _ilEmitter;
        private ControlFlowDesigner _controlFlowDesigner;

        public IfStatementDesigner(CilEmitter ilEmitter, ControlFlowDesigner controlFlowDesigner, Label endBranchLabel)
        {
            _ilEmitter = ilEmitter;
            _controlFlowDesigner = controlFlowDesigner;
            _endBranchLabel = endBranchLabel;
        }

        public IfStatementDesigner IsNull(LocalBuilder localVariable)
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