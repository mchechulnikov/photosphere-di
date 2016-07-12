using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;

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

        public IReadOnlyCollection<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            return _assembly.GetCustomAttributes(typeof(TAttribute)).Select(a => (TAttribute) a).ToHashSet();
        }
    }
}