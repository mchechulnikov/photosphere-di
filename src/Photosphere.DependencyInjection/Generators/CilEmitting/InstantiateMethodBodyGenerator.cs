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
            LocalBuilder instanceVariableFromScope;
            if (scope.AvailableInstancesVariables.TryGetValue(objectGraph.ImplementationType, out instanceVariableFromScope))
            {
                _ilGenerator.PushToStack(instanceVariableFromScope);
            }
            else
            {
                instanceVariableFromScope = _ilGenerator.DeclareLocalVariableOf(objectGraph.ImplementationType);
                CreateNewInstance(objectGraph);
                _ilGenerator.PopFromStackTo(instanceVariableFromScope); // TODO Is there any stack peek instruction in CIL?
                scope.Add(objectGraph.ImplementationType, instanceVariableFromScope);
                _ilGenerator.PushToStack(instanceVariableFromScope);
            }
        }

        private void GenerateForPerContainerScope(IPerContainerScope scope, IObjectGraph objectGraph)
        {
            var instanceFromScope = scope.AvailableInstances.SingleOrDefault(o => o.GetType() == objectGraph.ImplementationType);
            if (instanceFromScope != null)
            {
                _ilGenerator.PushToStackFirstArgument();
                var arrayIndex = scope.AvailableInstances.IndexOf(instanceFromScope);
                _ilGenerator.PushToStack(arrayIndex);
                _ilGenerator.PushToStackArrayElementAsRef();
            }
            else
            {
                var arrayIndex = scope.AvailableInstances.IsEmpty()
                    ? 0
                    : scope.AvailableInstances.IndexOf(scope.AvailableInstances.LastOrDefault()) + 1;
                var instanceVariableFromScope = _ilGenerator.DeclareLocalVariableOf(typeof(object));

                CreateNewInstance(objectGraph);
                _ilGenerator.PopFromStackTo(instanceVariableFromScope);

                _ilGenerator.PushToStackFirstArgument();
                _ilGenerator.PushToStack(arrayIndex);
                _ilGenerator.PushToStack(instanceVariableFromScope);
                _ilGenerator.ReplaceArrayElementAtIndexWithRefValueFromStack();

                _ilGenerator.PushToStack(instanceVariableFromScope);
            }
        }

        private object Do(object[] objects)
        {
            var result = new List<int>();
            objects[42] = result;
            return result;
        }
    }
}