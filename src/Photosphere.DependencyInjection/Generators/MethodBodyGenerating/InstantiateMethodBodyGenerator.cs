using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Lifetimes;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating
{
    internal class InstantiateMethodBodyGenerator : IInstantiateMethodBodyGenerator
    {
        private readonly IScopeKeeper _scopeKeeper;
        private ControlFlowBuilder _controlFlowBuilder;

        public InstantiateMethodBodyGenerator(ICilEmitter ilEmitter, IScopeKeeper scopeKeeper)
        {
            _controlFlowBuilder = new ControlFlowBuilder(ilEmitter);
            _scopeKeeper = scopeKeeper;
        }

        public void Generate(IObjectGraph objectGraph)
        {
            var resultVariable = GenerateForGraph(objectGraph);
            _controlFlowBuilder.ReturnStatement(resultVariable);
        }

        private LocalBuilder GenerateForGraph(IObjectGraph objectGraph)
        {
            return _controlFlowBuilder
                .DeclareVariable(objectGraph.ReturnType)
                .AssignTo(v => GenerateInstantiating(objectGraph))
                .Variable;
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
            var elementType = objectGraph.ImplementationType.GetElementType();

            _controlFlowBuilder
                .CreateNewArray(elementType, parameters.Count)
                .FillArray(parameters);
        }

        private void CreateNewInstance(IObjectGraph objectGraph)
        {
            var parameters = EmitParameters(objectGraph);
            _controlFlowBuilder.CreateNewObject(objectGraph.Constructor, parameters);
        }

        private void GenerateForPerRequestScope(IObjectGraph objectGraph)
        {
            var scope = _scopeKeeper.PerRequestScope;
            LocalBuilder instanceVariable;
            if (!scope.AvailableInstancesVariables.TryGetValue(objectGraph.ImplementationType, out instanceVariable))
            {
                instanceVariable = _controlFlowBuilder
                    .DeclareVariable(objectGraph.ImplementationType)
                    .AssignTo(v =>
                    {
                        scope.Add(objectGraph.ImplementationType, v);
                        CreateNewInstance(objectGraph);
                    })
                    .Variable;
            }
            _controlFlowBuilder.PushToStack(instanceVariable);
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

            var instanceVariable = _controlFlowBuilder.DeclareVariable<object>().Variable;

            _controlFlowBuilder
                .LoadArgumentToStack(0)
                .LoadArrayRefElementTo(instanceVariable, instanceIndex)
                .If().IsNull(instanceVariable)
                .BeginBranch()
                    .Action(() => CreateNewInstance(objectGraph))
                    .PopFromStackTo(instanceVariable)
                    .LoadArgumentToStack(0)
                    .SetArrayRefElement(instanceIndex, instanceVariable)
                .EndBranch()
                .PushToStack(instanceVariable)
                .CastToClass(objectGraph.ImplementationType);
        }

        private IEnumerable<LocalBuilder> EmitParameters(IObjectGraph objectGraph)
        {
            return objectGraph.Children.Select(GenerateForGraph);
        }
    }
}