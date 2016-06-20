using System;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generation.MethodBodyGenerating.Designers
{
    internal struct VariableDesigner
    {
        private CilEmitter _ilEmitter;

        public VariableDesigner(CilEmitter ilEmitter, Type type)
        {
            _ilEmitter = ilEmitter;
            Variable = ilEmitter.DeclareLocalVariableOf(type);
        }

        public LocalBuilder Variable { get; }

        public VariableDesigner AssignTo(Action<LocalBuilder> assignAction)
        {
            assignAction(Variable);
            _ilEmitter.Emit(OpCodes.Stloc, Variable);
            return this;
        }
    }
}