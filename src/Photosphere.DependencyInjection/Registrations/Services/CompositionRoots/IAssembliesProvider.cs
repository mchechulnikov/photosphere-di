using System.Collections.Generic;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots
{
    internal interface IAssembliesProvider
    {
        IEnumerable<IAssemblyWrapper> Provide();
    }
}