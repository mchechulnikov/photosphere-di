using System.Collections.Generic;

namespace Photosphere.Registration.Services
{
    internal interface ICompositionRootFinder
    {
        IReadOnlyList<ICompositionRoot> Find();
    }
}