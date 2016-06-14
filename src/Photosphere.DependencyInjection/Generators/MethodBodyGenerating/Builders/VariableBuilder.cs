using System;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct VariableBuilder
    {
        private readonly CfgBuilder _cfgBuilder;
        private readonly ICilEmitter _ilEmitter;

        public VariableBuilder(CfgBuilder cfgBuilder, ICilEmitter ilEmitter, Type type)
        {
            _cfgBuilder = cfgBuilder;
            _ilEmitter = ilEmitter;
            Variable = ilEmitter.DeclareLocalVariableOf(type);
        }

        public LocalBuilder Variable { get; private set; }

        public VariableBuilder AssignTo(Action<LocalBuilder> assignAction)
        {
            assignAction(Variable);
            _ilEmitter.Emit(OpCodes.Stloc, Variable);
            return this;
        }
    }
}