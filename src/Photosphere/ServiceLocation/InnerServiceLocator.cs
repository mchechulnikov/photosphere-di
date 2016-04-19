using System;
using System.Collections.Generic;
using Photosphere.CilEmitting.Factories;
using Photosphere.CilEmitting.Services;
using Photosphere.Registration.Services;
using Photosphere.Registration.ValueObjects;

namespace Photosphere.ServiceLocation
{
    internal class InnerServiceLocator : IInnerServiceLocator
    {
        private static readonly IReadOnlyDictionary<Type, object> InnerServices = new Dictionary<Type, object>
        {
            { typeof(ICompositionRootFinder), new CompositionRootFinder() },
            { typeof(IRegistrator), new Registrator(new Registry(), new InstantiateMethodGenerator(new MethodBodyEmitterFactory())) },
        };

        public TService GetInstance<TService>()
        {
            return (TService) InnerServices[typeof(TService)];
        }
    }
}