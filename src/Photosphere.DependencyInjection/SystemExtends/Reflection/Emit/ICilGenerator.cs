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
        void PushToStack(int number);
        void PushToStack(IEnumerable<LocalBuilder> localVariables);
        void PushToStackFirstArgument();
        void PushToStackArrayElementAsRef();
        void ReturnStatement(LocalBuilder variableForReturn);
        void ReplaceArrayElementAtIndexWithRefValueFromStack();
        LocalBuilder DeclareLocalVariableOf(Type type);
        void DuplicateValueOnTopOfStack();
        void BranchToTarget(Label label);
        void Pop();

        Label DefineLabel();
        void MarkLabel(Label label);

        ILGenerator Generator { get; }
    }
}