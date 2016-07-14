using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Photosphere.DependencyInjection.Extensions;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal class AssemblyWrapper : IAssemblyWrapper
    {
        public AssemblyWrapper(Assembly assembly)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; }

        public string FullName => Assembly.FullName;

        public IEnumerable<Type> Types => Assembly.GetTypes();

        public IReadOnlyCollection<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute
        {
            return Assembly.GetCustomAttributes(typeof(TAttribute)).Select(a => (TAttribute) a).ToHashSet();
        }
    }
}