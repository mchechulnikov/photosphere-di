using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;

namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating
{
    internal interface IInstantiateMethodBodyGenerator
    {
        void Generate(IObjectGraph objectGraph);
    }
}