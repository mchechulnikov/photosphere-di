using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct CfgBuilder
    {
        private readonly ICilEmitter _ilEmitter;

        public CfgBuilder(ICilEmitter ilEmitter)
        {
            _ilEmitter = ilEmitter;
        }

        public CfgBuilder PushToStack(LocalBuilder localVariable)
        {
            _ilEmitter.Emit(OpCodes.Ldloc, localVariable);
            return this;
        }

        public CfgBuilder PopFromStackTo(LocalBuilder localVariable)
        {
            _ilEmitter.Emit(OpCodes.Stloc, localVariable);
            return this;
        }

        public CfgBuilder CreateNewObject(ConstructorInfo constructor, IEnumerable<LocalBuilder> parametersVariables)
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

        public CfgBuilder ReturnStatement(LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Ret);
            return this;
        }

        public CfgBuilder LoadArgumentToStack(int argumentIndex)
        {
            _ilEmitter.Emit(OpCodes.Ldarg, argumentIndex);
            return this;
        }

        public CfgBuilder LoadArrayRefElementTo(int index, LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, index)
                .Emit(OpCodes.Ldelem_Ref)
                .Emit(OpCodes.Stloc, localVariable);
            return this;
        }

        public CfgBuilder SetArrayRefElement(int index, LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, index)
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Stelem_Ref);
            return this;
        }

        public CfgBuilder CompareWithNull(LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Ldnull)
                .Emit(OpCodes.Ceq);
            return this;
        }

        public CfgBuilder IfFalseJumpToLabel(Label label)
        {
            _ilEmitter.Emit(OpCodes.Brfalse, label);
            return this;
        }

        public CfgBuilder CastToClass(Type targetType)
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

        public IfBuilder If(Func<CfgBuilder, LocalBuilder> conditionAction)
        {
            return new IfBuilder(this, _ilEmitter, conditionAction);
        }
    }
}