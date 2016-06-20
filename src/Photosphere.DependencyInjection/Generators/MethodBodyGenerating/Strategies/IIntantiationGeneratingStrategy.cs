namespace Photosphere.DependencyInjection.Generators.MethodBodyGenerating.Strategies
{
    internal interface IIntantiationGeneratingStrategy : IGeneratingStrategy
    {
        void GenerateNewInstance(GeneratingDesign design);
    }
}