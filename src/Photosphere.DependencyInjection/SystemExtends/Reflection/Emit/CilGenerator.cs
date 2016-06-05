using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.SystemExtends.Reflection.Emit
{
    internal class CilGenerator : ICilGenerator
    {
        private readonly ILGenerator _systemIlGenerator;

        public CilGenerator(ILGenerator systemIlGenerator)
        {
            _systemIlGenerator = systemIlGenerator;
        }

        public void CreateNewInstanceBy(ConstructorInfo constructor)
        {
            _systemIlGenerator.Emit(OpCodes.Newobj, constructor);
        }

        public void PopFromStackTo(LocalBuilder localVariable)
        {
            _systemIlGenerator.Emit(OpCodes.Stloc, localVariable);
        }

        public void PushToStack(LocalBuilder localVariable)
        {
            _systemIlGenerator.Emit(OpCodes.Ldloc, localVariable);
        }

        public void PushToStack(int number)
        {
            _systemIlGenerator.Emit(OpCodes.Ldc_I4_S, number);
        }

        public void PushToStack(IEnumerable<LocalBuilder> localVariables)
        {
            foreach (var localVariable in localVariables)
            {
                PushToStack(localVariable);
            }
        }

        public void PushToStackFirstArgument()
        {
            _systemIlGenerator.Emit(OpCodes.Ldarg_1);
        }

        public void PushToStackArrayElementAsRef()
        {
            _systemIlGenerator.Emit(OpCodes.Ldelem_Ref);
        }

        public void ReturnStatement(LocalBuilder variableForReturn)
        {
            PushToStack(variableForReturn);
            _systemIlGenerator.Emit(OpCodes.Ret);
        }

        public void ReplaceArrayElementAtIndexWithRefValueFromStack()
        {
            _systemIlGenerator.Emit(OpCodes.Stelem_Ref);
        }

        public LocalBuilder DeclareLocalVariableOf(Type type)
        {
            return _systemIlGenerator.DeclareLocal(type);
        }
    }
}