using System;
using System.Collections.Generic;
using System.Reflection;

namespace Photosphere.DependencyInjection.StaticServices.DataTransferObjects
{
    internal class TypeConstructorInfo
    {
        public ConstructorInfo Constructor { get; set; }

        public IReadOnlyList<Type> ParametersTypes { get; set; }
    }
}