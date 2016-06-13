using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.CilEmitting
{
    internal class InstantiateMethodBodyGenerator : IInstantiateMethodBodyGenerator
    {
        private readonly IFluentCilGenerator _ilGenerator;
        private readonly IScopeKeeper _scopeKeeper;

        public InstantiateMethodBodyGenerator(IFluentCilGenerator ilGenerator, IScopeKeeper scopeKeeper)
        {
            _ilGenerator = ilGenerator;
            _scopeKeeper = scopeKeeper;
        }

        public IFluentCilGenerator Generate(IObjectGraph objectGraph)
        {
            var resultVariable = GenerateForGraph(objectGraph);
            _ilGenerator.Emit(OpCodes.Ldloc, resultVariable);
            _ilGenerator.Emit(OpCodes.Ret);
            return _ilGenerator;
        }

        private LocalBuilder GenerateForGraph(IObjectGraph objectGraph)
        {
            var resultVariable = _ilGenerator.DeclareLocalVariableOf(objectGraph.ReturnType);
            GenerateInstantiating(objectGraph);
            _ilGenerator.Emit(OpCodes.Stloc, resultVariable);
            return resultVariable;
        }

        private void GenerateInstantiating(IObjectGraph objectGraph)
        {
            if (objectGraph.IsEnumerable)
            {
                CreateNewArrayInstance(objectGraph);
                return;
            }
            switch (objectGraph.Lifetime)
            {
                case Lifetime.AlwaysNew:
                    CreateNewInstance(objectGraph);
                    return;
                case Lifetime.PerRequest:
                    GenerateForPerRequestScope(objectGraph);
                    return;
                case Lifetime.PerContainer:
                    GenerateForPerContainerScope(objectGraph);
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CreateNewArrayInstance(IObjectGraph objectGraph)
        {
            var parameters = EmitParameters(objectGraph).ToList();

            _ilGenerator
                .Emit(OpCodes.Ldc_I4, objectGraph.Children.Count)
                .Emit(OpCodes.Newarr, objectGraph.ImplementationType.GetElementType());

            var index = 0;
            foreach (var parameter in parameters)
            {
                _ilGenerator
                    .Emit(OpCodes.Dup)
                    .Emit(OpCodes.Ldc_I4, index)
                    .Emit(OpCodes.Ldloc, parameter)
                    .Emit(OpCodes.Stelem_Ref);
                index++;
            }
        }

        private void CreateNewInstance(IObjectGraph objectGraph)
        {
            var parameters = EmitParameters(objectGraph);
            _ilGenerator
                .Emit(OpCodes.Ldloc, parameters)
                .Emit(OpCodes.Newobj, objectGraph.Constructor);
        }

        private void GenerateForPerRequestScope(IObjectGraph objectGraph)
        {
            var scope = _scopeKeeper.PerRequestScope;
            LocalBuilder instanceVariable;
            if (scope.AvailableInstancesVariables.TryGetValue(objectGraph.ImplementationType, out instanceVariable))
            {
                _ilGenerator.Emit(OpCodes.Ldloc, instanceVariable);
            }
            else
            {
                instanceVariable = _ilGenerator.DeclareLocalVariableOf(objectGraph.ImplementationType);
                scope.Add(objectGraph.ImplementationType, instanceVariable);

                CreateNewInstance(objectGraph);
                _ilGenerator
                    .Emit(OpCodes.Stloc, instanceVariable)
                    .Emit(OpCodes.Ldloc, instanceVariable);
            }
        }

        private void GenerateForPerContainerScope(IObjectGraph objectGraph)
        {
            var scope = _scopeKeeper.PerContainerScope;
            int instanceIndex;
            if (!scope.AvailableInstancesIndexes.TryGetValue(objectGraph.ImplementationType, out instanceIndex))
            {
                instanceIndex = scope.AvailableInstancesIndexes.Count;
                scope.AvailableInstancesIndexes.Add(objectGraph.ImplementationType, instanceIndex);
            }

            var instanceVariable = _ilGenerator.DeclareLocalVariableOf(typeof(object));
            var booleanVariable = _ilGenerator.DeclareLocalVariableOf(typeof(bool));

            var branchLabel = _ilGenerator.DefineLabel();

            _ilGenerator
                .Emit(OpCodes.Ldarg_0)
                .Emit(OpCodes.Ldc_I4, instanceIndex)
                .Emit(OpCodes.Ldelem_Ref)
                .Emit(OpCodes.Stloc, instanceVariable)
                .Emit(OpCodes.Ldloc, instanceVariable)
                .Emit(OpCodes.Ldnull)
                .Emit(OpCodes.Ceq)
                .Emit(OpCodes.Stloc, booleanVariable)
                .Emit(OpCodes.Ldloc, booleanVariable)
                .Emit(OpCodes.Brfalse, branchLabel);

            CreateNewInstance(objectGraph);

            _ilGenerator
                .Emit(OpCodes.Stloc, instanceVariable)
                .Emit(OpCodes.Ldarg_0)
                .Emit(OpCodes.Ldc_I4, instanceIndex)
                .Emit(OpCodes.Ldloc, instanceVariable)
                .Emit(OpCodes.Stelem_Ref)
                .MarkLabel(branchLabel)
                .Emit(OpCodes.Ldloc, instanceVariable)
                .Emit(OpCodes.Castclass, objectGraph.ImplementationType);
        }

        private IEnumerable<LocalBuilder> EmitParameters(IObjectGraph objectGraph)
        {
            return objectGraph.Children.Select(GenerateForGraph);
        }
    }
}