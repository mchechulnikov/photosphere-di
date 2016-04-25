using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registration.Services.CompositionRoots
{
    internal interface IAssembliesProvider
    {
        IEnumerable<Assembly> Provide();
    }
}