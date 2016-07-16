using System;
using System.Collections.Generic;
using System.Linq;
using Photosphere.DependencyInjection.SystemExtends.Reflection;

namespace Photosphere.DependencyInjection.Initialization.Analysis.Composition.CompositionRoots.Exceptions
{
    internal class SeveralCompositionRootsWasFoundException : Exception
    {
        private readonly string _assemblyName;
        private readonly string _typesNames;

        public SeveralCompositionRootsWasFoundException(IAssemblyWrapper assembly, IEnumerable<Type> types)
        {
            _assemblyName = assembly.FullName;
            _typesNames = types.Select(t => t.FullName).Aggregate((s1, s2) => $"`{s1}`, `{s2}`");
        }

        public override string Message => $"More than one composition roots was found in assembly `{_assemblyName}`: {_typesNames}";
    }
}