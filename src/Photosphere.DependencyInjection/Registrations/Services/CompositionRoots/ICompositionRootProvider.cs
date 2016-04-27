using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots
{
    internal interface ICompositionRootProvider
    {
        IEnumerable<ICompositionRoot> Provide();
    }
}