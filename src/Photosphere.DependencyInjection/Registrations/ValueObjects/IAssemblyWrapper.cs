using System;
using System.Collections.Generic;

namespace Photosphere.DependencyInjection.Registrations.ValueObjects
{
    internal interface IAssemblyWrapper
    {
        string FullName { get; }

        IEnumerable<Type> Types { get; }
    }
}