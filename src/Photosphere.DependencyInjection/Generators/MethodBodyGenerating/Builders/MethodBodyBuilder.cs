using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct MethodBodyBuilder
    {
        private readonly ICilEmitter _ilEmitter;

        public MethodBodyBuilder(ICilEmitter ilEmitter)
        {
            _ilEmitter = ilEmitter;
        }

        public MethodBodyBuilder PushToStack(LocalBuilder localVariable)
        {
            _ilEmitter.Emit(OpCodes.Ldloc, localVariable);
            return this;
        }

        public MethodBodyBuilder PopFromStackTo(LocalBuilder localVariable)
        {
            _ilEmitter.Emit(OpCodes.Stloc, localVariable);
            return this;
        }

        public MethodBodyBuilder CreateNewObject(ConstructorInfo constructor, IEnumerable<LocalBuilder> parametersVariables)
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

        public MethodBodyBuilder ReturnStatement(LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Ret);
            return this;
        }

        public MethodBodyBuilder LoadArgumentToStack(int argumentIndex)
        {
            _ilEmitter.Emit(OpCodes.Ldarg, argumentIndex);
            return this;
        }

        public MethodBodyBuilder LoadArrayRefElement(int index)
        {
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, index)
                .Emit(OpCodes.Ldelem_Ref);
            return this;
        }

        public MethodBodyBuilder SetArrayRefElement(int index, LocalBuilder localVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, index)
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Stelem_Ref);
            return this;
        }

        public MethodBodyBuilder CompareWithNull(LocalBuilder localVariable, LocalBuilder resultBooleanVariable)
        {
            _ilEmitter
                .Emit(OpCodes.Ldloc, localVariable)
                .Emit(OpCodes.Ldnull)
                .Emit(OpCodes.Ceq)
                .Emit(OpCodes.Stloc, resultBooleanVariable);
            return this;
        }

        public MethodBodyBuilder IfFalseJumpToLabel(Label label)
        {
            _ilEmitter.Emit(OpCodes.Brfalse, label);
            return this;
        }

        public MethodBodyBuilder CastToClass(Type targetType)
        {
            _ilEmitter.Emit(OpCodes.Castclass, targetType);
            return this;
        }

        public VariableBuilder DeclareVariable(Type type)
        {
            return new VariableBuilder(_ilEmitter, type);
        }
    }

    internal struct IfBuilder
    {
        private readonly ICilEmitter _ilEmitter;
        private readonly LocalBuilder _conditionBooleanBuilder;

        public IfBuilder(ICilEmitter ilEmitter, LocalBuilder conditionBooleanBuilder)
        {
            _ilEmitter = ilEmitter;
            _conditionBooleanBuilder = conditionBooleanBuilder;
        }
    }

    internal struct ConditionBuilder
    {
        private readonly ICilEmitter _ilEmitter;

        public ConditionBuilder(ICilEmitter ilEmitter)
        {
            _ilEmitter = ilEmitter;
        }
    }
}