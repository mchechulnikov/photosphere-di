using System.Collections.Generic;
using System.Linq;
using Photosphere.ServiceLocating.Extensions;
using Photosphere.ServiceLocating.Metadata;
using Photosphere.ServiceLocating.Templates;

namespace Photosphere.ServiceLocating.Generating
{
    internal class VariablesGenerator
    {
        private readonly IReadOnlyCollection<ClassMetadata> _classes;

        public VariablesGenerator(IReadOnlyCollection<ClassMetadata> classes)
        {
            _classes = classes;
        }

        public string Generate(string className, IReadOnlyCollection<string> parametersTypes, ISet<string> alreadyActivatedList)
        {
            if (parametersTypes == null)
            {
                return "\t\t\t" + string.Format(
                    TemplatesResource.VariableStatement,
                    className.ToLowerCamelCase(),
                    string.Format(TemplatesResource.NewInstanceStatement, className, string.Empty)
                ) + "\r\n";
            }

            var result = string.Empty;
            var parametersList = new List<string>();
            var parameterClassMetadatas = GetParameterClassInfos(parametersTypes);
            foreach (var parameterClassMetadata in parameterClassMetadatas)
            {
                var varName = parameterClassMetadata.ClassName.ToLowerCamelCase();
                parametersList.Add(varName);

                if (alreadyActivatedList.Contains(varName))
                {
                    continue;
                }
                alreadyActivatedList.Add(varName);
                result += Generate(
                    parameterClassMetadata.ClassName,
                    parameterClassMetadata.CtorParametersTypesNames?.ToArray(),
                    alreadyActivatedList
                );
            }
            result += "\t\t\t" + string.Format(
                TemplatesResource.VariableStatement,
                className.ToLowerCamelCase(),
                string.Format(TemplatesResource.NewInstanceStatement, className, string.Join(", ", parametersList))
            ) + "\r\n";
            return result;
        }

        private IEnumerable<ClassMetadata> GetParameterClassInfos(IEnumerable<string> parametersTypes) =>
            parametersTypes
                .Select(pt => _classes.FirstOrDefault(x => x.BaseTypesNames != null && x.BaseTypesNames.Contains(pt)))
                .Where(x => x != null);
    }
}