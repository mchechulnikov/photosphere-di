using Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies;
using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating
{
    internal interface IGeneratingStrategyProvider
    {
        IGeneratingStrategy Provide(IObjectGraph objectGraph);
    }
}