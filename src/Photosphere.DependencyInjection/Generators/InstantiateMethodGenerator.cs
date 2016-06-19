using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Builders;
using Photosphere.DependencyInjection.Generators.ObjectGraphs;

namespace Photosphere.DependencyInjection.Generators
{
    internal class InstantiateMethodGenerator : IInstantiateMethodGenerator
    {
        private readonly IObjectGraphProvider _objectGraphProvider;
        private readonly IInstantiateMethodBodyGenerator _methodBodyGenerator;

        public InstantiateMethodGenerator(
            IObjectGraphProvider objectGraphPovider,
            IInstantiateMethodBodyGenerator methodBodyGenerator)
        {
            _objectGraphProvider = objectGraphPovider;
            _methodBodyGenerator = methodBodyGenerator;
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
            _methodBodyGenerator.Generate(new GeneratingDesign
            {
                Designer = new ControlFlowDesigner(dynamicMethod.GetILGenerator()),
                ObjectGraph = _objectGraphProvider.Provide(targetType)
            });
        }

        private static Delegate CreateDelegate(MethodInfo dynamicMethod, Type serviceType)
        {
            var delegateType = Expression.GetFuncType(typeof(object[]), serviceType);
            return dynamicMethod.CreateDelegate(delegateType);
        }
    }
}