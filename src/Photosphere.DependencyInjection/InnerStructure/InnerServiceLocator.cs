using Photosphere.DependencyInjection.Generators;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.InstantiatingGenerators;
using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Services.Strategies;
using Photosphere.DependencyInjection.Generators.ObjectGraphs;
using Photosphere.DependencyInjection.Lifetimes.Scopes.Services;
using Photosphere.DependencyInjection.Registrations.Services;
using Photosphere.DependencyInjection.Registrations.Services.CompositionRoots;
using Photosphere.DependencyInjection.Registrations.ValueObjects;
using Photosphere.DependencyInjection.Resolving;

namespace Photosphere.DependencyInjection.InnerStructure
{
    internal class InnerServiceLocator
    {
        public InnerServiceLocator()
        {
            var registry = new Registry();
            var scopeKeeper = new ScopeKeeper();
            var resolver = new Resolver(registry, scopeKeeper);
            var assembliesProvider = new AssembliesProvider();
            var compositionRootProvider = new CompositionRootProvider(assembliesProvider);

            var objectInstantiatingGenerator = new ObjectInstantiatingGenerator();
            var arrayInstantiatingGenerator = new ArrayInstantiatingGenerator();
            var generatingStrategyProvider = new GeneratingStrategyProvider(
                new IntantiationProvidingGeneratingStrategy(objectInstantiatingGenerator), 
                new PerRequestProvidingGeneratingStrategy(scopeKeeper, objectInstantiatingGenerator),
                new PerContainerProvidingGeneratingStrategy(scopeKeeper, objectInstantiatingGenerator),
                new EnumerableProvidingGeneratingStrategy(arrayInstantiatingGenerator)
            );

            var objectGraphProvider = new ObjectGraphProvider(registry, generatingStrategyProvider);
            var instantiateMethodBodyGenerator = new InstantiateMethodBodyGenerator();
            var instantiateMethodGenerator = new InstantiateMethodGenerator(objectGraphProvider, instantiateMethodBodyGenerator);

            var registrationFactory = new RegistrationFactory(instantiateMethodGenerator);
            var registrator = new Registrator(registry, registrationFactory);
            var registryInitializer = new RegistryInitializer(compositionRootProvider, registrator, registry, scopeKeeper);

            ScopeKeeper = scopeKeeper;
            Resolver = resolver;
            RegistryInitializer = registryInitializer;
        }

        public IScopeKeeper ScopeKeeper { get; }
        public IResolver Resolver { get; }
        public IRegistryInitializer RegistryInitializer { get; }
    }
}