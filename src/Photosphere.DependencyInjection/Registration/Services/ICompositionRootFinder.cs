using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registration.Services
{
    internal interface ICompositionRootFinder
    {
        IReadOnlyList<ICompositionRoot> Find();
    }
}