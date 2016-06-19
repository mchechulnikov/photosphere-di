using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct ControlFlowBuilder
    {
        private readonly ICilEmitter _ilEmitter;
        private Label _endBranchLabel;

        public ControlFlowBuilder(ICilEmitter ilEmitter)
        {
            _ilEmitter = ilEmitter;

            IntoBranch = false;
            _endBranchLabel = _ilEmitter.DefineLabel();
        }

        public bool IntoBranch { get; set; }

        public ControlFlowBuilder PushToStack(LocalBuilder localVariable)
        {
            _ilEmitter.Emit(OpCodes.Ldloc, localVariable);
            return this;
        }

        public ControlFlowBuilder PopFromStackTo(LocalBuilder localVariable)
        {
            _ilEmitter.Emit(OpCodes.Stloc, localVariable);
            return this;
        }

        public ControlFlowBuilder CreateNewObject(ConstructorInfo constructor, IEnumerable<LocalBuilder> parametersVariables)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, parametersVariables)
                .Emit(OpCodes.Newobj, constructor);
            return this;
        }

        public ArrayBuilder CreateNewArray(Type elementType, int elementsCount)
        {
            return new ArrayBuilder(_ilEmitter, elementType, elementsCount);
        }

        public ControlFlowBuilder ReturnStatement(LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Ret);
            return this;
        }

        public ControlFlowBuilder LoadArgumentToStack(int argumentIndex)
        {
            _ilEmitter.Emit(OpCodes.Ldarg, argumentIndex);
            return this;
        }

        public ControlFlowBuilder LoadArrayRefElementTo(LocalBuilder localVariable, int index)
        {
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, index)
                .Emit(OpCodes.Ldelem_Ref)
                .Emit(OpCodes.Stloc, localVariable);
            return this;
        }

        public ControlFlowBuilder SetArrayRefElement(int index, LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, index)
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Stelem_Ref);
            return this;
        }

        public ControlFlowBuilder CastToClass(Type targetType)
        {
            _ilEmitter.Emit(OpCodes.Castclass, targetType);
            return this;
        }

        public VariableBuilder DeclareVariable(Type type)
        {
            return new VariableBuilder(this, _ilEmitter, type);
        }

        public VariableBuilder DeclareVariable<T>()
        {
            return new VariableBuilder(this, _ilEmitter, typeof(T));
        }

        public IfStatementBuilder If()
        {
            _endBranchLabel = _ilEmitter.DefineLabel();
            return new IfStatementBuilder(_ilEmitter, this, _endBranchLabel);
        }

        public ControlFlowBuilder EndBranch()
        {
            if (!IntoBranch)
            {
                throw new InvalidOperationException("Must be into the branch");
            }

            _ilEmitter.MarkLabel(_endBranchLabel);
            return this;
        }

        public ControlFlowBuilder Action(Action action)
        {
            action();
            return this;
        }
    }
}