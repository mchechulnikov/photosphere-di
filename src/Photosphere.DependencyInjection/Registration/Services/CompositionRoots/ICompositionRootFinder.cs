using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registration.Services.CompositionRoots
{
    internal interface ICompositionRootFinder
    {
        IEnumerable<ICompositionRoot> Find();
    }
}