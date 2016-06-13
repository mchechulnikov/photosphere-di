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
        private readonly ICilEmitter _ilEmitter;
        private readonly IScopeKeeper _scopeKeeper;
        private MethodBodyBuilder _methodBodyBuilder;

        public InstantiateMethodBodyGenerator(ICilEmitter ilEmitter, IScopeKeeper scopeKeeper)
        {
            _ilEmitter = ilEmitter;
            _methodBodyBuilder = new MethodBodyBuilder(_ilEmitter);
            _scopeKeeper = scopeKeeper;
        }

        public void Generate(IObjectGraph objectGraph)
        {
            var resultVariable = GenerateForGraph(objectGraph);
            _methodBodyBuilder.ReturnStatement(resultVariable);
        }

        private LocalBuilder GenerateForGraph(IObjectGraph objectGraph)
        {
            return _methodBodyBuilder
                .DeclareVariable(objectGraph.ReturnType)
                .Assign(v => GenerateInstantiating(objectGraph))
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

            _methodBodyBuilder
                .CreateNewArray(elementType, parameters.Count)
                .FillArray(parameters);
        }

        private void CreateNewInstance(IObjectGraph objectGraph)
        {
            var parameters = EmitParameters(objectGraph);
            _methodBodyBuilder.CreateNewObject(objectGraph.Constructor, parameters);
        }

        private void GenerateForPerRequestScope(IObjectGraph objectGraph)
        {
            var scope = _scopeKeeper.PerRequestScope;
            LocalBuilder instanceVariable;
            if (!scope.AvailableInstancesVariables.TryGetValue(objectGraph.ImplementationType, out instanceVariable))
            {
                instanceVariable = _methodBodyBuilder
                    .DeclareVariable(objectGraph.ImplementationType)
                    .Assign(v =>
                    {
                        scope.Add(objectGraph.ImplementationType, v);
                        CreateNewInstance(objectGraph);
                    })
                    .Variable;
            }
            _methodBodyBuilder.PushToStack(instanceVariable);
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

            var instanceVariable = _methodBodyBuilder.DeclareVariable(typeof(object)).Variable;
            var booleanVariable = _methodBodyBuilder.DeclareVariable(typeof(bool)).Variable;

            var branchLabel = _ilEmitter.DefineLabel();

            _methodBodyBuilder
                .LoadArgumentToStack(0)
                .LoadArrayRefElement(instanceIndex)
                .PopFromStackTo(instanceVariable)
                .CompareWithNull(instanceVariable, booleanVariable)
                .PushToStack(booleanVariable)
                .IfFalseJumpToLabel(branchLabel);

            CreateNewInstance(objectGraph);
            _methodBodyBuilder
                .PopFromStackTo(instanceVariable)
                .LoadArgumentToStack(0)
                .SetArrayRefElement(instanceIndex, instanceVariable);

            _ilEmitter.MarkLabel(branchLabel);

            _methodBodyBuilder
                .PushToStack(instanceVariable)
                .CastToClass(objectGraph.ImplementationType);
        }

        private IEnumerable<LocalBuilder> EmitParameters(IObjectGraph objectGraph)
        {
            return objectGraph.Children.Select(GenerateForGraph);
        }
    }
}