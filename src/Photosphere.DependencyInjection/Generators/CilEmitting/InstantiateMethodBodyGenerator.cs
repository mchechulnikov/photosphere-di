using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Lifetimes.Scopes;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.CilEmitting
{
    internal class InstantiateMethodBodyGenerator : IInstantiateMethodBodyGenerator
    {
        private readonly ICilGenerator _ilGenerator;
        private readonly IScopeKeeper _scopeKeeper;

        public InstantiateMethodBodyGenerator(ICilGenerator ilGenerator, IScopeKeeper scopeKeeper)
        {
            _ilGenerator = ilGenerator;
            _scopeKeeper = scopeKeeper;
        }

        public ICilGenerator Generate(IObjectGraph objectGraph)
        {
            var resultVariable = GenerateForGraph(objectGraph);
            _ilGenerator.ReturnStatement(resultVariable);
            return _ilGenerator;
        }

        private LocalBuilder GenerateForGraph(IObjectGraph objectGraph)
        {
            var resultVariable = _ilGenerator.DeclareLocalVariableOf(objectGraph.ImplementationType);
            var localVariables = EmitParameters(objectGraph);
            _ilGenerator.PushToStack(localVariables);
            GenerateInstantiating(objectGraph);
            _ilGenerator.PopFromStackTo(resultVariable);
            return resultVariable;
        }

        private IEnumerable<LocalBuilder> EmitParameters(IObjectGraph objectGraph)
        {
            return objectGraph.Children.Select(GenerateForGraph);
        }

        private void GenerateInstantiating(IObjectGraph objectGraph)
        {
            switch (objectGraph.Lifetime)
            {
                case Lifetime.AlwaysNew:
                    CreateNewInstance(objectGraph);
                    return;
                case Lifetime.PerRequest:
                    GenerateForPerRequestScope(_scopeKeeper.PerRequestScope, objectGraph);
                    return;
                case Lifetime.PerContainer:
                    GenerateForPerContainerScope(_scopeKeeper.PerContainerScope, objectGraph);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateNewInstance(IObjectGraph objectGraph)
        {
            _ilGenerator.CreateNewInstanceBy(objectGraph.Constructor);
        }

        private void GenerateForPerRequestScope(IPerRequestScope scope, IObjectGraph objectGraph)
        {
            LocalBuilder instanceVariable;
            if (scope.AvailableInstancesVariables.TryGetValue(objectGraph.ImplementationType, out instanceVariable))
            {
                _ilGenerator.PushToStack(instanceVariable);
            }
            else
            {
                instanceVariable = _ilGenerator.DeclareLocalVariableOf(objectGraph.ImplementationType);
                CreateNewInstance(objectGraph);
                _ilGenerator.PopFromStackTo(instanceVariable);
                scope.Add(objectGraph.ImplementationType, instanceVariable);
                _ilGenerator.PushToStack(instanceVariable);
            }
        }

        private void GenerateForPerContainerScope(IPerContainerScope scope, IObjectGraph objectGraph)
        {
            int instanceIndex;
            if (!scope.AvailableInstancesIndexes.TryGetValue(objectGraph.ImplementationType, out instanceIndex))
            {
                instanceIndex = scope.AvailableInstancesIndexes.Count;
                scope.AvailableInstancesIndexes.Add(objectGraph.ImplementationType, instanceIndex);
            }

            // final
            var instanceVariable = _ilGenerator.DeclareLocalVariableOf(typeof(object));
            var booleanVariable = _ilGenerator.DeclareLocalVariableOf(typeof(bool));

            var branchLabel = _ilGenerator.DefineLabel();
            var exitLabel = _ilGenerator.DefineLabel();

            _ilGenerator.Generator.Emit(OpCodes.Ldarg_1);
            _ilGenerator.Generator.Emit(OpCodes.Ldc_I4_S, instanceIndex);
            _ilGenerator.Generator.Emit(OpCodes.Ldelem_Ref);
            _ilGenerator.Generator.Emit(OpCodes.Ldnull);
            _ilGenerator.Generator.Emit(OpCodes.Cgt_Un);
            _ilGenerator.Generator.Emit(OpCodes.Stloc, booleanVariable);
            _ilGenerator.Generator.Emit(OpCodes.Ldloc, booleanVariable);
            _ilGenerator.Generator.Emit(OpCodes.Brfalse_S, branchLabel);
            _ilGenerator.Generator.Emit(OpCodes.Br_S, exitLabel);
            _ilGenerator.MarkLabel(branchLabel);
            CreateNewInstance(objectGraph);
            _ilGenerator.PopFromStackTo(instanceVariable);
            _ilGenerator.PushToStackFirstArgument();
            _ilGenerator.PushToStack(instanceIndex);
            _ilGenerator.PushToStack(instanceVariable);
            _ilGenerator.ReplaceArrayElementAtIndexWithRefValueFromStack();
            _ilGenerator.MarkLabel(exitLabel);
            _ilGenerator.PushToStack(instanceVariable);
        }

        private void Do(object[] objects)
        {
            const int index = 42;

            if (objects[index] != null)
            {
                return;
            }
            var result = new PerContainerScope();
            objects[index] = result;
        }
    }
}