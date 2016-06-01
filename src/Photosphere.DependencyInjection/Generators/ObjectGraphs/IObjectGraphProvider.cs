using System;
using System.Collections.Generic;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Generators.ObjectGraphs
{
    internal interface IObjectGraphProvider
    {
        IObjectGraph Provide(
            Type serviceType, Type implType, IRegistry registry, ISet<Type> alreadyProvidedTypes = null);
    }
}