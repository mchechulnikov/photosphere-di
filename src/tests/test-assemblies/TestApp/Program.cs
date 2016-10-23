using System;
using Photosphere.DependencyInjection;
using TestAssembly.Stress;

namespace TestApp
{
    /// <summary>
    /// This app used for profiling DI functionality
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = new DependencyContainer();
            var instances = container.GetAllInstances<IStressService>();
        }
    }
}
