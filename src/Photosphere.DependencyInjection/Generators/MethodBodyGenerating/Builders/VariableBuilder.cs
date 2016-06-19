using System;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct VariableBuilder
    {
        private CilEmitter _ilEmitter;

        public VariableBuilder(CilEmitter ilEmitter, Type type)
        {
            _ilEmitter = ilEmitter;
            Variable = ilEmitter.DeclareLocalVariableOf(type);
        }

        public LocalBuilder Variable { get; }

        public VariableBuilder AssignTo(Action<LocalBuilder> assignAction)
        {
            assignAction(Variable);
            _ilEmitter.Emit(OpCodes.Stloc, Variable);
            return this;
        }
    }
}