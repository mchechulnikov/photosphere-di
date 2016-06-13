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
            var resultVariable = _ilGenerator.DeclareLocalVariableOf(objectGraph.ReturnType);
            GenerateInstantiating(objectGraph);
            _ilGenerator.PopFromStackTo(resultVariable);
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

            _ilGenerator.Generator.Emit(OpCodes.Ldc_I4, objectGraph.Children.Count);
            _ilGenerator.Generator.Emit(OpCodes.Newarr, objectGraph.ImplementationType.GetElementType());

            var index = 0;
            foreach (var parameter in parameters)
            {
                _ilGenerator.Generator.Emit(OpCodes.Dup);
                _ilGenerator.Generator.Emit(OpCodes.Ldc_I4, index);
                _ilGenerator.Generator.Emit(OpCodes.Ldloc, parameter);
                _ilGenerator.Generator.Emit(OpCodes.Stelem_Ref);
                index++;
            }
        }

        private void CreateNewInstance(IObjectGraph objectGraph)
        {
            var parameters = EmitParameters(objectGraph);
            _ilGenerator.PushToStack(parameters);
            _ilGenerator.CreateNewInstanceBy(objectGraph.Constructor);
        }

        private void GenerateForPerRequestScope(IObjectGraph objectGraph)
        {
            var scope = _scopeKeeper.PerRequestScope;
            LocalBuilder instanceVariable;
            if (scope.AvailableInstancesVariables.TryGetValue(objectGraph.ImplementationType, out instanceVariable))
            {
                _ilGenerator.PushToStack(instanceVariable);
            }
            else
            {
                instanceVariable = _ilGenerator.DeclareLocalVariableOf(objectGraph.ImplementationType);
                scope.Add(objectGraph.ImplementationType, instanceVariable);

                CreateNewInstance(objectGraph);
                _ilGenerator.PopFromStackTo(instanceVariable);
                _ilGenerator.PushToStack(instanceVariable);
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

            _ilGenerator.Generator.Emit(OpCodes.Ldarg_0);
            _ilGenerator.Generator.Emit(OpCodes.Ldc_I4, instanceIndex);
            _ilGenerator.Generator.Emit(OpCodes.Ldelem_Ref);
            _ilGenerator.Generator.Emit(OpCodes.Stloc, instanceVariable);
            _ilGenerator.Generator.Emit(OpCodes.Ldloc, instanceVariable);
            _ilGenerator.Generator.Emit(OpCodes.Ldnull);
            _ilGenerator.Generator.Emit(OpCodes.Ceq);
            _ilGenerator.Generator.Emit(OpCodes.Stloc, booleanVariable);
            _ilGenerator.Generator.Emit(OpCodes.Ldloc, booleanVariable);
            _ilGenerator.Generator.Emit(OpCodes.Brfalse, branchLabel);

            CreateNewInstance(objectGraph);

            _ilGenerator.Generator.Emit(OpCodes.Stloc, instanceVariable);

            _ilGenerator.Generator.Emit(OpCodes.Ldarg_0);
            _ilGenerator.Generator.Emit(OpCodes.Ldc_I4, instanceIndex);
            _ilGenerator.Generator.Emit(OpCodes.Ldloc, instanceVariable);
            _ilGenerator.Generator.Emit(OpCodes.Stelem_Ref);
            _ilGenerator.MarkLabel(branchLabel);
            _ilGenerator.PushToStack(instanceVariable);
            _ilGenerator.Generator.Emit(OpCodes.Castclass, objectGraph.ImplementationType);
        }

        private IEnumerable<LocalBuilder> EmitParameters(IObjectGraph objectGraph)
        {
            return objectGraph.Children.Select(GenerateForGraph);
        }
    }
}