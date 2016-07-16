using System.Collections.Generic;
using Photosphere.DependencyInjection.SystemExtends.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Analysis.Assemblies
{
    internal interface IAssembliesProvider
    {
        IEnumerable<IAssemblyWrapper> Provide();
    }
}