using System.Collections.Generic;

namespace Photosphere.ServiceLocating.Metadata
{
    internal class ClassMetadata
    {
        public ClassMetadata(
            string className,
            string @namespace,
            IReadOnlyCollection<string> baseTypesNames,
            IReadOnlyCollection<string> ctorParametersTypesNames)
        {
            ClassName = className;
            Namespace = @namespace;
            BaseTypesNames = baseTypesNames;
            CtorParametersTypesNames = ctorParametersTypesNames;
        }

        public string ClassName { get; }

        public string Namespace { get; }

        public IReadOnlyCollection<string> BaseTypesNames { get; }

        public IReadOnlyCollection<string> CtorParametersTypesNames { get; }
    }
}