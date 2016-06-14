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
        private CfgBuilder _cfgBuilder;

        public InstantiateMethodBodyGenerator(ICilEmitter ilEmitter, IScopeKeeper scopeKeeper)
        {
            _ilEmitter = ilEmitter;
            _cfgBuilder = new CfgBuilder(_ilEmitter);
            _scopeKeeper = scopeKeeper;
        }

        public void Generate(IObjectGraph objectGraph)
        {
            var resultVariable = GenerateForGraph(objectGraph);
            _cfgBuilder.ReturnStatement(resultVariable);
        }

        private LocalBuilder GenerateForGraph(IObjectGraph objectGraph)
        {
            return _cfgBuilder
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

            _cfgBuilder
                .CreateNewArray(elementType, parameters.Count)
                .FillArray(parameters);
        }

        private void CreateNewInstance(IObjectGraph objectGraph)
        {
            var parameters = EmitParameters(objectGraph);
            _cfgBuilder.CreateNewObject(objectGraph.Constructor, parameters);
        }

        private void GenerateForPerRequestScope(IObjectGraph objectGraph)
        {
            var scope = _scopeKeeper.PerRequestScope;
            LocalBuilder instanceVariable;
            if (!scope.AvailableInstancesVariables.TryGetValue(objectGraph.ImplementationType, out instanceVariable))
            {
                instanceVariable = _cfgBuilder
                    .DeclareVariable(objectGraph.ImplementationType)
                    .AssignTo(v =>
                    {
                        scope.Add(objectGraph.ImplementationType, v);
                        CreateNewInstance(objectGraph);
                    })
                    .Variable;
            }
            _cfgBuilder.PushToStack(instanceVariable);
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

            var instanceVariable = _cfgBuilder.DeclareVariable<object>().Variable;

            _cfgBuilder
                .LoadArgumentToStack(0)
                .LoadArrayRefElementTo(instanceIndex, instanceVariable)
                .If(cfgBuilder =>
                {
                    return cfgBuilder
                        .DeclareVariable<bool>()
                        .AssignTo(v => _cfgBuilder.CompareWithNull(instanceVariable))
                        .Variable;
                })
                .Then(cfgBuilder =>
                {
                    CreateNewInstance(objectGraph);
                    cfgBuilder
                        .PopFromStackTo(instanceVariable)
                        .LoadArgumentToStack(0)
                        .SetArrayRefElement(instanceIndex, instanceVariable);
                })
                .PushToStack(instanceVariable)
                .CastToClass(objectGraph.ImplementationType);
        }

        private IEnumerable<LocalBuilder> EmitParameters(IObjectGraph objectGraph)
        {
            return objectGraph.Children.Select(GenerateForGraph);
        }
    }
}