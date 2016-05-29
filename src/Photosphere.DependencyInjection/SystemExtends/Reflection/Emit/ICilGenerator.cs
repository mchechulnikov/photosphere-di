using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Photosphere.DependencyInjection.SystemExtends.Reflection.Emit
{
    internal interface ICilGenerator
    {
        void CreateNewInstanceBy(ConstructorInfo constructor);
        void PopFromStackTo(LocalBuilder localVariable);
        void PushToStack(LocalBuilder localVariable);
        void PushToStack(IEnumerable<LocalBuilder> localVariables);
        void ReturnStatement(LocalBuilder variableForReturn);
        LocalBuilder DeclareLocalVariableOf(Type type);
    }
}