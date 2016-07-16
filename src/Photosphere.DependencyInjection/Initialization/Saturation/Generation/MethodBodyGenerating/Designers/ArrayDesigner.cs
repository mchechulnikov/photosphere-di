using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Designers
{
    internal struct ArrayDesigner
    {
        private readonly CilEmitter _ilEmitter;

        public ArrayDesigner(CilEmitter ilEmitter, Type elementType, int elementsCount)
        {
            _ilEmitter = ilEmitter;
            _ilEmitter
                .Emit(OpCodes.Ldc_I4, elementsCount)
                .Emit(OpCodes.Newarr, elementType);
        }

        public void FillArray(IEnumerable<LocalBuilder> localVariables)
        {
            var index = 0;
            foreach (var localVariable in localVariables)
            {
                _ilEmitter
                    .Emit(OpCodes.Dup)
                    .Emit(OpCodes.Ldc_I4, index)
                    .Emit(OpCodes.Ldloc, localVariable)
                    .Emit(OpCodes.Stelem_Ref);
                index++;
            }
        }
    }
}