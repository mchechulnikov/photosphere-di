using System.Collections.Generic;
using System.IO;
using System.Linq;
using Photosphere.ServiceLocating.Extensions;
using Photosphere.ServiceLocating.FileSystem;
using Photosphere.ServiceLocating.Generating;
using Photosphere.ServiceLocating.Metadata;
using Photosphere.ServiceLocating.Parsing;
using Photosphere.ServiceLocating.Templates;

namespace Photosphere.ServiceLocating
{
    public class ServiceLocatorGenerator
    {
        private readonly string[] _dependencies;
        private readonly SourceFilesContentReader _sourceFilesContentReader;

        public ServiceLocatorGenerator(string hostProvidedPath, params string[] dependencies)
        {
            _dependencies = dependencies;
            _sourceFilesContentReader = new SourceFilesContentReader(hostProvidedPath, SourceFilesExtension.CSharp);
        }

        public string Generate()
        {
            var filesContents = _sourceFilesContentReader.Read();
            var classesMetadata = GetClassesMetadata(filesContents);
            var variablesGenerator = new VariablesGenerator(classesMetadata);

            var generateUsingsDirectives = GenerateUsingsDirectives(classesMetadata);
            return string.Format(
                TemplatesResource.ServiceLocator,
                generateUsingsDirectives,
                string.Empty,
                "IContainerConfiguration containerConfiguration",
                GenerateConstructor(classesMetadata, variablesGenerator)
            );
        }

        private static string GenerateUsingsDirectives(IEnumerable<ClassMetadata> classesMetadata) =>
            classesMetadata
                .Select(t => t.Namespace)
                .Where(n => n != null)
                .Distinct()
                .Aggregate(string.Empty, (c, ns) => c + string.Format(TemplatesResource.UsingDirective, ns) + "\r\n");

        private string GenerateConstructor(IEnumerable<ClassMetadata> classesMetadata, VariablesGenerator variablesGenerator)
        {
            var result = string.Empty;
            var alreadyActivated = new HashSet<string>();
            alreadyActivated.Add("containerConfiguration");
            foreach (
                var type in classesMetadata.Where(x => x.BaseTypesNames != null && Contains(x.BaseTypesNames, _dependencies)))
            {
                var serviceName = _dependencies.First(x => type.BaseTypesNames.Contains(x));
                if (type.CtorParametersTypesNames != null)
                {
                    result += variablesGenerator.Generate(type.ClassName, type.CtorParametersTypesNames, alreadyActivated);
                }
                else
                {
                    var varName = type.ClassName.ToLowerCamelCase();
                    if (!alreadyActivated.Contains(varName))
                    {
                        result += "\t\t\t" + string.Format(
                            TemplatesResource.VariableStatement,
                            varName,
                            string.Format(TemplatesResource.NewInstanceStatement, type.ClassName, string.Empty)
                        ) + "\r\n";
                        continue;
                    }
                    alreadyActivated.Add(varName);
                }
                result += "\t\t\t" + string.Format(
                    TemplatesResource.AddToDictinaryStatement,
                    "_map",
                    $"typeof({serviceName})",
                    type.ClassName.ToLowerCamelCase()
                ) + "\r\n";
            }
            return result;
        }

        private static IReadOnlyCollection<ClassMetadata> GetClassesMetadata(IEnumerable<string> filesContents) =>
            filesContents
                .Select(File.ReadAllText)
                .Select(CSharpFileParser.Parse)
                .Where(info => info != null).ToList();

        private static bool Contains(IEnumerable<string> source, IEnumerable<string> target) => target.Any(source.Contains);
    }
}