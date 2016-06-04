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
            if (objectGraph.Lifetime == Lifetime.AlwaysNew)
            {
                CreateNewInstance(objectGraph);
                return;
            }
            GenerateInstantiatingForScopedLifetime(objectGraph);
        }

        private void GenerateInstantiatingForScopedLifetime(IObjectGraph objectGraph)
        {
            var scope = _scopeKeeper.Provide(objectGraph.Lifetime);
            LocalBuilder instanceVaraiableFromScope;
            if (scope.AvailableInstancesVariables.TryGetValue(objectGraph.ImplementationType, out instanceVaraiableFromScope))
            {
                _ilGenerator.PushToStack(instanceVaraiableFromScope);
            }
            else
            {
                instanceVaraiableFromScope = _ilGenerator.DeclareLocalVariableOf(objectGraph.ImplementationType);
                CreateNewInstance(objectGraph);
                _ilGenerator.PopFromStackTo(instanceVaraiableFromScope); // TODO Is there any stack peek instruction in CIL?
                scope.Add(objectGraph.ImplementationType, instanceVaraiableFromScope);
                _ilGenerator.PushToStack(instanceVaraiableFromScope);
            }
        }

        private void CreateNewInstance(IObjectGraph objectGraph)
        {
            _ilGenerator.CreateNewInstanceBy(objectGraph.Constructor);
        }
    }
}