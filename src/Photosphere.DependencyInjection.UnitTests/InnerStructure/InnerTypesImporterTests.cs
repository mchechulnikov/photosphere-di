using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace Photosphere.DependencyInjection.UnitTests.InnerStructure
{
    public class InnerTypesImporterTests
    {
        private const string TypeDefinitionExample = "public class Foo : FooBase, IFoo, IBar";

        [Fact]
        internal void Test1()
        {
            Directory.GetFiles("dfd", "df", SearchOption.AllDirectories);
            var value = new Regex("class [a-zA-Z0-9]+").Match("dfd").Value;
            var actualString = value.Substring(6);
            Assert.Contains("Foo", actualString);
        }

        [Fact]
        internal void Test2()
        {
            var expected = new[] { "FooBase", "IFoo", "IBar" };
            foreach (var s in expected)
            {
                Assert.Contains(s,
                    new Regex(":( )*([a-zA-Z0-9]+(, )*)+")
                    .Match(TypeDefinitionExample).Value
                    .TrimStart(':').TrimStart().Replace(",", string.Empty).Split(' '));
            }
        }
    }
}