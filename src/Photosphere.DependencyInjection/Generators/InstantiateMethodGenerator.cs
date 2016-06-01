using System;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.CilEmitting;
using Photosphere.DependencyInjection.Generators.ObjectGraphs;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.ValueObjects;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators
{
    internal class InstantiateMethodGenerator : IInstantiateMethodGenerator
    {
        private readonly IRegistry _registry;
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IObjectGraphProvider _objectGraphProvider;

        public InstantiateMethodGenerator(
            IRegistry registry,
            IScopeKeeper scopeKeeper,
            IObjectGraphProvider objectGraphPovider)
        {
            _registry = registry;
            _scopeKeeper = scopeKeeper;
            _objectGraphProvider = objectGraphPovider;
        }

        public Func<TTarget> Generate<TTarget>()
        {
            var dynamicMethod = CreateDynamicMethod<TTarget>();
            Generate<TTarget>(_registry, dynamicMethod);
            return CreateDelegate<TTarget>(dynamicMethod);
        }

        private static DynamicMethod CreateDynamicMethod<TTarget>()
        {
            var targetType = typeof(TTarget);
            var methodName = $"CreateInstanceOf{targetType.Name}";
            return new DynamicMethod(methodName, targetType, null, true);
        }

        private void Generate<TTarget>(IRegistry registry, DynamicMethod dynamicMethod)
        {
            var objectGraph = GetObjectGraph<TTarget>(registry);
            var ilGenerator = new CilGenerator(dynamicMethod.GetILGenerator());
            var methodBodyGenerator = new InstantiateMethodBodyGenerator(ilGenerator, _scopeKeeper);
            methodBodyGenerator.Generate(objectGraph);
        }

        private IObjectGraph GetObjectGraph<TTarget>(IRegistry registry)
        {
            var serviceType = typeof(TTarget);
            var implementationType = serviceType.GetFirstImplementationType();
            return _objectGraphProvider.Provide(serviceType, implementationType, registry);
        }

        private static Func<TTarget> CreateDelegate<TTarget>(MethodInfo dynamicMethod)
        {
            return (Func<TTarget>) dynamicMethod.CreateDelegate(typeof(Func<TTarget>));
        }
    }
}