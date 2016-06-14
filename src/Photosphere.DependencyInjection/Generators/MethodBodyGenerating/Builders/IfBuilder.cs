using System;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders
{
    internal struct IfBuilder
    {
        private readonly ICilEmitter _ilEmitter;
        private readonly Label _label;
        private readonly CfgBuilder _cfgBuilder;

        public IfBuilder(CfgBuilder cfgBuilder, ICilEmitter ilEmitter, Func<CfgBuilder, LocalBuilder> conditionAction)
        {
            _cfgBuilder = cfgBuilder;
            _ilEmitter = ilEmitter;
            _label = _ilEmitter.DefineLabel();

            var booleanVariable = conditionAction(cfgBuilder);
            _ilEmitter.Emit(OpCodes.Ldloc, booleanVariable);
            _ilEmitter.Emit(OpCodes.Brfalse, _label);
        }

        public CfgBuilder Then(Action<CfgBuilder> action)
        {
            action(_cfgBuilder);
            _ilEmitter.MarkLabel(_label);
            return _cfgBuilder;
        }
    }
}