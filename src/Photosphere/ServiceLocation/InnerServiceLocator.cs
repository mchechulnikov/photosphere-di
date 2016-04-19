using System;
using System.Collections.Generic;
using Photosphere.Registration.Services;

namespace Photosphere.ServiceLocation
{
    internal class InnerServiceLocator : IInnerServiceLocator
    {
        private static readonly IReadOnlyDictionary<Type, object> InnerServices = new Dictionary<Type, object>
        {
            { typeof(ICompositionRootFinder), new CompositionRootFinder() },
        };

        public TService GetInstance<TService>()
        {
            return (TService) InnerServices[typeof(TService)];
        }
    }
}