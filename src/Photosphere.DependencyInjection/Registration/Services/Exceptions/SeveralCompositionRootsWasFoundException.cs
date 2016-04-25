using System;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registration.Services.Exceptions
{
    internal class SeveralCompositionRootsWasFoundException : Exception
    {
        private readonly string _assemblyName;

        public SeveralCompositionRootsWasFoundException(Assembly assembly)
        {
            _assemblyName = assembly.FullName;
        }

        public override string Message => $"More than one composition roots was found in assembly `{_assemblyName}`";
    }
}