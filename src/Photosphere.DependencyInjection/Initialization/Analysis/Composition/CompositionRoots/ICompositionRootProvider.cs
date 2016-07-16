using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots
{
    internal interface ICompositionRootProvider
    {
        IEnumerable<ICompositionRoot> Provide();
    }
}