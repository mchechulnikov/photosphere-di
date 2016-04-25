using System;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registration.Services.Exceptions
{
    internal class CompositionRootNotFoundException : Exception
    {
        private readonly string _assemblyName;

        public CompositionRootNotFoundException(Assembly assembly)
        {
            _assemblyName = assembly.FullName;
        }

        public override string Message => $"Composition root not found in assembly `{_assemblyName}`";
    }
}