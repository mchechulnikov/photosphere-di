using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.SystemExtends.Reflection.Emit
{
    internal interface ICilEmitter
    {
        ICilEmitter Emit(OpCode opCode);
        ICilEmitter Emit(OpCode opCode, int integer);
        ICilEmitter Emit(OpCode opCode, Type type);
        ICilEmitter Emit(OpCode opCode, ConstructorInfo constructor);
        ICilEmitter Emit(OpCode opCode, LocalBuilder localVariableBuilder);
        ICilEmitter Emit(OpCode opCode, IEnumerable<LocalBuilder> localVariableBuilders);
        ICilEmitter Emit(OpCode opCode, Label label);
        ICilEmitter MarkLabel(Label label);
        LocalBuilder DeclareLocalVariableOf(Type type, bool isPinned = false);
        Label DefineLabel();
    }
}