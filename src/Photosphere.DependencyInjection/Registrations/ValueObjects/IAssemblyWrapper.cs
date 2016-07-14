using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal interface IAssemblyWrapper
    {
        Assembly Assembly { get; }

        string FullName { get; }

        IEnumerable<Type> Types { get; }

        IReadOnlyCollection<TAttribute> GetAttributes<TAttribute>() where TAttribute : Attribute;
    }
}