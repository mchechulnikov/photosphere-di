using System;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct VariableBuilder
    {
        private readonly ICilEmitter _ilEmitter;

        public VariableBuilder(ICilEmitter ilEmitter, Type type)
        {
            _ilEmitter = ilEmitter;
            Variable = ilEmitter.DeclareLocalVariableOf(type);
        }

        public LocalBuilder Variable { get; }

        public VariableBuilder Assign(Action<LocalBuilder> assignAction)
        {
            assignAction(Variable);
            _ilEmitter.Emit(OpCodes.Stloc, Variable);
            return this;
        }
    }
}