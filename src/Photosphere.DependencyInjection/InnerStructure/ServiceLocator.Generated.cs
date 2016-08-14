using System;
using System.Collections.Generic;
using Photosphere.DependencyInjection;
using Photosphere.DependencyInjection.Initialization;
using Photosphere.DependencyInjection.Initialization.Analysis.Assemblies;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots.Exceptions;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots.ServiceCompositionRoots;
using Photosphere.DependencyInjection.Initialization.Registrations;
using Photosphere.DependencyInjection.Initialization.Registrations.ValueObjects;
using Photosphere.DependencyInjection.Initialization.Saturation;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.GeneratingStrategies;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.MethodBodyGenerating.Services.InstantiatingGenerators;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Initialization.Saturation.Generation.ObjectGraphs.Exceptions;
using Photosphere.DependencyInjection.InnerStructure;
using Photosphere.DependencyInjection.LifetimeManagement;
using Photosphere.DependencyInjection.LifetimeManagement.Scopes;
using Photosphere.DependencyInjection.Resolving;
using Photosphere.DependencyInjection.SystemExtends.Reflection;
using Photosphere.DependencyInjection.Types;

namespace Photosphere.DependencyInjection.InnerStructure
{
	internal class ServiceLocator
	{
		private readonly IDictionary<Type, object> _map = new Dictionary<Type, object>();

		public ServiceLocator(IContainerConfiguration containerConfiguration)
		{
			var assembliesProvider = new AssembliesProvider(containerConfiguration);
			var compositionRootProvider = new CompositionRootProvider(assembliesProvider);
			var registry = new Registry();
			var typesProvider = new TypesProvider();
			var generatingStrategyProvider = new GeneratingStrategyProvider(null, null, null, null);
			var objectGraphProvider = new ObjectGraphProvider(registry, generatingStrategyProvider);
			var instanceProvidingMethodBodyGenerator = new InstanceProvidingMethodBodyGenerator();
			var instanceProvidingMethodGenerator = new InstanceProvidingMethodGenerator(objectGraphProvider, instanceProvidingMethodBodyGenerator);
			var registrationFactory = new RegistrationFactory(typesProvider, instanceProvidingMethodGenerator);
			var assemblyBoundedRegistrator = new AssemblyBoundedRegistrator(registry, registrationFactory);
			var registratorProvider = new RegistratorProvider(assemblyBoundedRegistrator);
			var dependenciesCompositor = new DependenciesCompositor(compositionRootProvider, registratorProvider);
			var scopeKeeper = new ScopeKeeper();
			var registrySaturator = new RegistrySaturator(registry, scopeKeeper);
			var registryInitializer = new RegistryInitializer(dependenciesCompositor, registrySaturator);
			_map.Add(typeof (IRegistryInitializer), registryInitializer);
			_map.Add(typeof (IScopeKeeper), scopeKeeper);
			var resolver = new Resolver(registry, scopeKeeper);
			_map.Add(typeof (IResolver), resolver);
		}

		public T Get<T>() where T : class => (T) _map[typeof(T)];
	}
}
