using Photosphere.DependencyInjection.Initialization;
using Photosphere.DependencyInjection.Initialization.Analysis.Assemblies;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots;
using Photosphere.DependencyInjection.Initialization.Registrations;
using Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects;
using Photosphere.DependencyInjection.Initialization.Saturation;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.GeneratingStrategies;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.InstantiatingGenerators;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs;
using Photosphere.DependencyInjection.LifetimeManagement;
using Photosphere.DependencyInjection.Resolving;

namespace Photosphere.DependencyInjection.InnerStructure
{
    internal class InnerServiceLocator
    {
        private InnerServiceLocator(IContainerConfiguration configuration)
        {
            var registry = new Registry();
            var scopeKeeper = new ScopeKeeper();
            var resolver = new Resolver(registry, scopeKeeper);

            var objectInstantiatingGenerator = new ObjectInstantiatingGenerator();
            var arrayInstantiatingGenerator = new ArrayInstantiatingGenerator();
            var generatingStrategyProvider = new GeneratingStrategyProvider(
                new IntantiationProvidingGeneratingStrategy(objectInstantiatingGenerator), 
                new PerRequestProvidingGeneratingStrategy(scopeKeeper, objectInstantiatingGenerator),
                new PerContainerProvidingGeneratingStrategy(scopeKeeper, objectInstantiatingGenerator),
                new EnumerableProvidingGeneratingStrategy(arrayInstantiatingGenerator)
            );

            var objectGraphProvider = new ObjectGraphProvider(registry, generatingStrategyProvider);
            var instantiateMethodBodyGenerator = new InstanceProvidingMethodBodyGenerator();
            var instantiateMethodGenerator = new InstanceProvidingMethodGenerator(
                objectGraphProvider,
                instantiateMethodBodyGenerator
            );

            var registrationFactory = new RegistrationFactory(instantiateMethodGenerator);

            var compositionRootProvider = new CompositionRootProvider(new AssembliesProvider(configuration));
            var registratorProvider = new RegistratorProvider(
                new AssemblyBoundedRegistrator(registry, registrationFactory),
                new InterceptorRegistrator()
            );
            var dependenciesCompositor = new DependenciesCompositor(compositionRootProvider, registratorProvider);
            var registrySaturator = new RegistrySaturator(registry, scopeKeeper);
            var registryInitializer = new RegistryInitializer(dependenciesCompositor, registrySaturator);

            ScopeKeeper = scopeKeeper;
            Resolver = resolver;
            RegistryInitializer = registryInitializer;
        }

        public static InnerServiceLocator New(IContainerConfiguration configuration = null)
        {
            return new InnerServiceLocator(configuration);
        }

        public IScopeKeeper ScopeKeeper { get; }
        public IResolver Resolver { get; }
        public IRegistryInitializer RegistryInitializer { get; }
    }
}