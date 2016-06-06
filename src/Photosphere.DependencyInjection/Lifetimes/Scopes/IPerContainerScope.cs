namespace Photosphere.DependencyInjection.Lifetimes.Scopes
{
    internal interface IPerContainerScope : IScope
    {
        int AvailableInstancesCount { set; }

        object[] AvailableInstances { get; }
    }
}