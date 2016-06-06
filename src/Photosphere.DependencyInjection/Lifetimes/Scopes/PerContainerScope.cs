namespace Photosphere.DependencyInjection.Lifetimes.Scopes
{
    internal class PerContainerScope : IPerContainerScope
    {
        private object[] _availableServices;

        public int AvailableInstancesCount { private get; set; }

        public object[] AvailableInstances => _availableServices ?? (_availableServices = new object[AvailableInstancesCount]);

        public void Dispose()
        {
            _availableServices = null;
        }
    }
}