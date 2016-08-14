using System.Linq;
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
            var value = new Regex("class [a-zA-Z0-9]+").Match(TypeDefinitionExample).Value;
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

        [Fact]
        internal void Test3()
        {
            const string str = @"
                public class Foo : FooBase, IFoo, IBar
                {
                  public Foo(this IBuz buz, IQiz qiz = null)
                  {
                  }
                }";
            var expected = new [] { "IBuz", "IQiz" };
            var result = new Regex("Foo\\(([(this) \\w=]+,*( )*)+")
                .Match(str).Value
                .Replace("Foo(", string.Empty)
                .Replace("this ", string.Empty)
                .Split(',')
                .Select(x => x.Trim())
                .Select(x => x.Split(' ').First());
            foreach (var s in expected)
            {
                Assert.Contains(s, result);
            }
        }
    }
}