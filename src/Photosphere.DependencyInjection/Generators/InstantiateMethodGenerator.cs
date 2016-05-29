using System;
using System.Reflection;
using System.Reflection.Emit;
using Photosphere.DependencyInjection.Extensions;
using Photosphere.DependencyInjection.Generators.CilEmitting;
using Photosphere.DependencyInjection.Generators.ObjectGraphs;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.InnerRegistry;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators
{
    // TODO Rewrite without static
    internal static class InstantiateMethodGenerator
    {
        public static Func<TTarget> Generate<TTarget>(IRegistry registry = null)
        {
            var dynamicMethod = CreateDynamicMethod<TTarget>();
            registry = registry ?? InnerRegistryProvider.InnerRegistry;
            var ilGenerator = dynamicMethod.GetILGenerator();
            var objectGraph = GetObjectGraph<TTarget>(registry);
            InstantiateMethodBodyEmitter.GenerateFor(ilGenerator, objectGraph);
            return CreateDelegate<TTarget>(dynamicMethod);
        }

        private static DynamicMethod CreateDynamicMethod<TTarget>()
        {
            var targetType = typeof(TTarget);
            var methodName = $"CreateInstanceOf{targetType.Name}";
            return new DynamicMethod(methodName, targetType, null, true);
        }

        private static Func<TTarget> CreateDelegate<TTarget>(MethodInfo dynamicMethod)
        {
            return (Func<TTarget>) dynamicMethod.CreateDelegate(typeof(Func<TTarget>));
        }

        private static IObjectGraph GetObjectGraph<TTarget>(IRegistry registry)
        {
            var implementationType = typeof(TTarget).GetFirstImplementationType();
            return ObjectGraphProvider.Provide(implementationType, registry);
        }
    }
}