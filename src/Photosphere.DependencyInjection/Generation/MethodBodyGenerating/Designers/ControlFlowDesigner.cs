using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Designers
{
    internal struct ControlFlowDesigner
    {
        private CilEmitter _ilEmitter;
        private Label _endBranchLabel;

        public ControlFlowDesigner(ILGenerator ilGenerator)
        {
            _ilEmitter = new CilEmitter(ilGenerator);

            IntoBranch = false;
            _endBranchLabel = _ilEmitter.DefineLabel();
        }

        public bool IntoBranch { get; set; }

        public ControlFlowDesigner PushToStack(LocalBuilder localVariable)
        {
            _ilEmitter.Emit(OpCodes.Ldloc, localVariable);
            return this;
        }

        public ControlFlowDesigner PopFromStackTo(LocalBuilder localVariable)
        {
            _ilEmitter.Emit(OpCodes.Stloc, localVariable);
            return this;
        }

        public ControlFlowDesigner CreateNewObject(ConstructorInfo constructor, IEnumerable<LocalBuilder> parametersVariables)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, parametersVariables)
                .Emit(OpCodes.Newobj, constructor);
            return this;
        }

        public ArrayDesigner CreateNewArray(Type elementType, int elementsCount)
        {
            return new ArrayDesigner(_ilEmitter, elementType, elementsCount);
        }

        public ControlFlowDesigner ReturnStatement(LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Ret);
            return this;
        }

        public ControlFlowDesigner LoadArgumentToStack(int argumentIndex)
        {
            _ilEmitter.Emit(OpCodes.Ldarg, argumentIndex);
            return this;
        }

        public ControlFlowDesigner LoadArrayRefElementTo(LocalBuilder localVariable, int index)
        {
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, index)
                .Emit(OpCodes.Ldelem_Ref)
                .Emit(OpCodes.Stloc, localVariable);
            return this;
        }

        public ControlFlowDesigner SetArrayRefElement(int index, LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, index)
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Stelem_Ref);
            return this;
        }

        public ControlFlowDesigner CastToClass(Type targetType)
        {
            _ilEmitter.Emit(OpCodes.Castclass, targetType);
            return this;
        }

        public VariableDesigner DeclareVariable(Type type)
        {
            return new VariableDesigner(_ilEmitter, type);
        }

        public VariableDesigner DeclareVariable<T>()
        {
            return new VariableDesigner(_ilEmitter, typeof(T));
        }

        public IfStatementDesigner If()
        {
            _endBranchLabel = _ilEmitter.DefineLabel();
            return new IfStatementDesigner(_ilEmitter, this, _endBranchLabel);
        }

        public ControlFlowDesigner EndBranch()
        {
            if (!IntoBranch)
            {
                throw new InvalidOperationException("Must be into the branch");
            }

            _ilEmitter.MarkLabel(_endBranchLabel);
            return this;
        }

        public ControlFlowDesigner Action(Action action)
        {
            action();
            return this;
        }
    }
}