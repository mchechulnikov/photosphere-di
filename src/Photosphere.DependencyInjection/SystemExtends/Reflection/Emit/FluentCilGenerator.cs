using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.SystemExtends.Reflection.Emit
{
    internal class FluentCilGenerator : IFluentCilGenerator
    {
        private readonly ILGenerator _systemIlGenerator;

        public FluentCilGenerator(ILGenerator systemIlGenerator)
        {
            _systemIlGenerator = systemIlGenerator;
        }

        public IFluentCilGenerator Emit(OpCode opCode)
        {
            _systemIlGenerator.Emit(opCode);
            return this;
        }

        public IFluentCilGenerator Emit(OpCode opCode, int integer)
        {
            _systemIlGenerator.Emit(opCode, integer);
            return this;
        }

        public IFluentCilGenerator Emit(OpCode opCode, Type type)
        {
            _systemIlGenerator.Emit(opCode, type);
            return this;
        }

        public IFluentCilGenerator Emit(OpCode opCode, ConstructorInfo constructor)
        {
            _systemIlGenerator.Emit(opCode, constructor);
            return this;
        }

        public IFluentCilGenerator Emit(OpCode opCode, LocalBuilder localVariableBuilder)
        {
            _systemIlGenerator.Emit(opCode, localVariableBuilder);
            return this;
        }

        public IFluentCilGenerator Emit(OpCode opCode, IEnumerable<LocalBuilder> localVariableBuilders)
        {
            foreach (var localVariableBuilder in localVariableBuilders)
            {
                _systemIlGenerator.Emit(opCode, localVariableBuilder);
            }
            return this;
        }

        public IFluentCilGenerator Emit(OpCode opCode, Label label)
        {
            _systemIlGenerator.Emit(opCode, label);
            return this;
        }

        public LocalBuilder DeclareLocalVariableOf(Type type)
        {
            return _systemIlGenerator.DeclareLocal(type);
        }

        public Label DefineLabel()
        {
            return _systemIlGenerator.DefineLabel();
        }

        public IFluentCilGenerator MarkLabel(Label label)
        {
            _systemIlGenerator.MarkLabel(label);
            return this;
        }
    }
}