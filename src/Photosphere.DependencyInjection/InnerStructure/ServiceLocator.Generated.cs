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
		private readonly IContainerConfiguration _configuration;
		private readonly IDictionary<Type, Type[]> _map = new Dictionary<Type, Type[]>
		{
			{ typeof (ContainerConfiguration), null },
			{ typeof (DependencyContainer), null },
			{ typeof (RegistryInitializer), null },
			{ typeof (AssembliesProvider), null },
			{ typeof (DependenciesCompositor), null },
			{ typeof (CompositionRootProvider), null },
			{ typeof (SeveralCompositionRootsWasFoundException), null },
			{ typeof (DefaultCompositionRoot), null },
			{ typeof (AssemblyBoundedRegistrator), null },
			{ typeof (RegistrationFactory), null },
			{ typeof (Registrator), null },
			{ typeof (RegistratorProvider), null },
			{ typeof (Registration), null },
			{ typeof (Registry), null },
			{ typeof (RegistrySaturator), null },
			{ typeof (InstanceProvidingMethodGenerator), null },
			{ typeof (InstanceProvidingMethodBodyGenerator), null },
			{ typeof (GeneratingStrategyProvider), null },
			{ typeof (EnumerableProvidingGeneratingStrategy), null },
			{ typeof (IntantiationProvidingGeneratingStrategy), null },
			{ typeof (PerContainerProvidingGeneratingStrategy), null },
			{ typeof (PerRequestProvidingGeneratingStrategy), null },
			{ typeof (ArrayInstantiatingGenerator), null },
			{ typeof (ObjectInstantiatingGenerator), null },
			{ typeof (ObjectGraphProvider), null },
			{ typeof (ObjectGraph), null },
			{ typeof (DetectedCycleDependencyException), null },
			{ typeof (TypeNotRegisteredException), null },
			{ typeof (InnerServiceLocator), null },
			{ typeof (ServiceLocator), null },
			{ typeof (ScopeKeeper), null },
			{ typeof (PerContainerScope), null },
			{ typeof (PerRequestScope), null },
			{ typeof (Resolver), null },
			{ typeof (AssemblyWrapper), null },
			{ typeof (TypesProvider), null },
		};

		public ServiceLocator(IContainerConfiguration configuration)
		{
			_configuration = configuration;

			var registry = new Registry();
			var scopeKeeper = new ScopeKeeper();
			var resolver = new Resolver(registry, scopeKeeper);
		}

		public T Get<T>() where T : class => null;
	}
}
