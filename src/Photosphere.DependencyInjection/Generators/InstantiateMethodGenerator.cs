using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.CilEmitting;
using Photosphere.DependencyInjection.Generators.ObjectGraphs;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators
{
    internal class InstantiateMethodGenerator : IInstantiateMethodGenerator
    {
        private readonly IScopeKeeper _scopeKeeper;
        private readonly IObjectGraphProvider _objectGraphProvider;

        public InstantiateMethodGenerator(
            IScopeKeeper scopeKeeper,
            IObjectGraphProvider objectGraphPovider)
        {
            _scopeKeeper = scopeKeeper;
            _objectGraphProvider = objectGraphPovider;
        }

        public Delegate Generate(Type serviceType)
        {
            var dynamicMethod = CreateDynamicMethod(serviceType);
            DefineParameters(dynamicMethod);
            Generate(dynamicMethod, serviceType);
            return CreateDelegate(dynamicMethod, serviceType);
        }

        private static DynamicMethod CreateDynamicMethod(Type targetType)
        {
            var methodName = $"PhotosphereDI_CreateInstance_Of_{targetType.GetFormattedName()}";
            return new DynamicMethod(methodName, targetType, new [] { typeof(object[]) }, true);
        }

        private static void DefineParameters(DynamicMethod dynamicMethod)
        {
            dynamicMethod.DefineParameter(1, ParameterAttributes.None, "perContainerInstances");
        }

        private void Generate(DynamicMethod dynamicMethod, Type targetType)
        {
            var objectGraph = _objectGraphProvider.Provide(targetType);
            var ilGenerator = new FluentCilGenerator(dynamicMethod.GetILGenerator());
            var methodBodyGenerator = new InstantiateMethodBodyGenerator(ilGenerator, _scopeKeeper);
            methodBodyGenerator.Generate(objectGraph);
        }

        private static Delegate CreateDelegate(MethodInfo dynamicMethod, Type serviceType)
        {
            var delegateType = Expression.GetFuncType(typeof(object[]), serviceType);
            return dynamicMethod.CreateDelegate(delegateType);
        }
    }
}