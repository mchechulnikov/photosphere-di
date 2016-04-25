using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots
{
    internal interface IAssembliesProvider
    {
        IEnumerable<Assembly> Provide();
    }
}