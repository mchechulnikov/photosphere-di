using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.SystemExtends.Reflection.Emit
{
    internal struct CilEmitter
    {
        private readonly ILGenerator _systemIlGenerator;

        public CilEmitter(ILGenerator systemIlGenerator)
        {
            _systemIlGenerator = systemIlGenerator;
        }

        public CilEmitter Emit(OpCode opCode)
        {
            _systemIlGenerator.Emit(opCode);
            return this;
        }

        public CilEmitter Emit(OpCode opCode, int integer)
        {
            _systemIlGenerator.Emit(opCode, integer);
            return this;
        }

        public CilEmitter Emit(OpCode opCode, Type type)
        {
            _systemIlGenerator.Emit(opCode, type);
            return this;
        }

        public CilEmitter Emit(OpCode opCode, ConstructorInfo constructor)
        {
            _systemIlGenerator.Emit(opCode, constructor);
            return this;
        }

        public CilEmitter Emit(OpCode opCode, LocalBuilder localVariableBuilder)
        {
            _systemIlGenerator.Emit(opCode, localVariableBuilder);
            return this;
        }

        public CilEmitter Emit(OpCode opCode, IEnumerable<LocalBuilder> localVariableBuilders)
        {
            foreach (var localVariableBuilder in localVariableBuilders)
            {
                _systemIlGenerator.Emit(opCode, localVariableBuilder);
            }
            return this;
        }

        public CilEmitter Emit(OpCode opCode, Label label)
        {
            _systemIlGenerator.Emit(opCode, label);
            return this;
        }

        public LocalBuilder DeclareLocalVariableOf(Type type, bool isPinned = false)
        {
            return _systemIlGenerator.DeclareLocal(type, isPinned);
        }

        public Label DefineLabel()
        {
            return _systemIlGenerator.DefineLabel();
        }

        public CilEmitter MarkLabel(Label label)
        {
            _systemIlGenerator.MarkLabel(label);
            return this;
        }
    }
}