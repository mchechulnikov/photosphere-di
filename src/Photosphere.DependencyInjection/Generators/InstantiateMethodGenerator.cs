using System;
using System.Linq.Expressions;
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

        public Delegate Generate(Type serviceType)
        {
            var dynamicMethod = CreateDynamicMethod(serviceType);
            DefineParameters(dynamicMethod);
            Generate(_registry, dynamicMethod, serviceType);
            return CreateDelegate(dynamicMethod, serviceType);
        }

        private static DynamicMethod CreateDynamicMethod(Type targetType)
        {
            var methodName = $"PhotosphereDI_CreateInstance_Of_{targetType.Name}";
            return new DynamicMethod(methodName, targetType, new [] { typeof(object[]) }, true);
        }

        private static void DefineParameters(DynamicMethod dynamicMethod)
        {
            dynamicMethod.DefineParameter(1, ParameterAttributes.None, "perContainerInstances");
        }

        private void Generate(IRegistry registry, DynamicMethod dynamicMethod, Type targetType)
        {
            var objectGraph = GetObjectGraph(registry, targetType);
            var ilGenerator = new CilGenerator(dynamicMethod.GetILGenerator());
            var methodBodyGenerator = new InstantiateMethodBodyGenerator(ilGenerator, _scopeKeeper);
            methodBodyGenerator.Generate(objectGraph);
        }

        private IObjectGraph GetObjectGraph(IRegistry registry, Type serviceType)
        {
            var implementationType = serviceType.GetFirstImplementationType();
            return _objectGraphProvider.Provide(serviceType, implementationType, registry);
        }

        private static Delegate CreateDelegate(MethodInfo dynamicMethod, Type serviceType)
        {
            var delegateType = Expression.GetFuncType(typeof(object[]), serviceType);
            return dynamicMethod.CreateDelegate(delegateType);
        }
    }
}