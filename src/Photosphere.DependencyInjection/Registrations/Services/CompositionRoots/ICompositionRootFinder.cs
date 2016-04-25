using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registrations.Services.CompositionRoots
{
    internal interface ICompositionRootFinder
    {
        IEnumerable<ICompositionRoot> Find();
    }
}