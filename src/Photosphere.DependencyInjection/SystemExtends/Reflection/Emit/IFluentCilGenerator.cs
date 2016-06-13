using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.SystemExtends.Reflection.Emit
{
    internal interface IFluentCilGenerator
    {
        IFluentCilGenerator Emit(OpCode opCode);
        IFluentCilGenerator Emit(OpCode opCode, int integer);
        IFluentCilGenerator Emit(OpCode opCode, Type type);
        IFluentCilGenerator Emit(OpCode opCode, ConstructorInfo constructor);
        IFluentCilGenerator Emit(OpCode opCode, LocalBuilder localVariableBuilder);
        IFluentCilGenerator Emit(OpCode opCode, IEnumerable<LocalBuilder> localVariableBuilders);
        IFluentCilGenerator Emit(OpCode opCode, Label label);
        IFluentCilGenerator MarkLabel(Label label);
        LocalBuilder DeclareLocalVariableOf(Type type);
        Label DefineLabel();
    }
}