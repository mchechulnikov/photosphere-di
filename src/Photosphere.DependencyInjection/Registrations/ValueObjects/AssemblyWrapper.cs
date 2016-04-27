using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal class AssemblyWrapper : IAssemblyWrapper
    {
        private readonly Assembly _assembly;

        public AssemblyWrapper(Assembly assembly)
        {
            _assembly = assembly;
        }

        public string FullName => _assembly.FullName;

        public IEnumerable<Type> Types => _assembly.GetTypes();
    }
}