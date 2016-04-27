using System;
using Photosphere.DependencyInjection.Registrations.ValueObjects;

namespace Photosphere.DependencyInjection.Registrations.Services.Exceptions
{
    internal class CompositionRootNotFoundException : Exception
    {
        private readonly string _assemblyName;

        public CompositionRootNotFoundException(IAssemblyWrapper assembly)
        {
            _assemblyName = assembly.FullName;
        }

        public override string Message => $"Composition root not found in assembly `{_assemblyName}`";
    }
}