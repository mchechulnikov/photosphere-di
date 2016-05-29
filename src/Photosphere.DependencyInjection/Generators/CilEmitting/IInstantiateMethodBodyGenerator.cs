using Photosphere.DependencyInjection.Generators.ObjectGraphs.DataTransferObjects;
using Photosphere.DependencyInjection.SystemExtends.Reflection.Emit;

namespace Photosphere.DependencyInjection.Generators.CilEmitting
{
    internal interface IInstantiateMethodBodyGenerator
    {
        ICilGenerator Generate(IObjectGraph objectGraph);
    }
}