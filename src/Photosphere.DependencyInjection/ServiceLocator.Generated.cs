using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection
{
	internal class ServiceLocator
	{
		private readonly IContainerConfiguration _configuration;
		private readonly IDictionary<Type, object> _map = new Dictionary<Type, object>
		{
			{typeof(Type), null},
			{typeof(Type), null},
			{typeof(Type), null},
			{typeof(Type), null},
			{typeof(Type), null},
		};

		public ServiceLocator(IContainerConfiguration configuration)
		{
			_configuration = configuration;
		}

		public T Get<T>() where T : class => null;
	}
}
