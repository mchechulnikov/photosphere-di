using System;
using Photosphere.DependencyInjection.Initialization.Analysis.Composition;
using Photosphere.DependencyInjection.Initialization.Saturation;

namespace Photosphere.DependencyInjection.Initialization
{
    internal class RegistryInitializer : IRegistryInitializer
    {
        private readonly IDependenciesCompositor _dependenciesCompositor;
        private readonly IRegistrySaturator _registrySaturator;

        public RegistryInitializer(
            IDependenciesCompositor dependenciesCompositor,
            IRegistrySaturator registrySaturator)
        {
            _dependenciesCompositor = dependenciesCompositor;
            _registrySaturator = registrySaturator;
        }

        public void Initialize()
        {
            try
            {
                _dependenciesCompositor.Compose();
                _registrySaturator.Saturate();
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException != null)
                {
                    throw aggregateException.InnerException;
                }
                throw;
            }
        }
    }
}