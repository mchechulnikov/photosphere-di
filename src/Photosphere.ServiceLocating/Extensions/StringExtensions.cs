using System.Text.RegularExpressions;

namespace Photosphere.ServiceLocating.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsWord(this string str) => new Regex("\\w").IsMatch(str);

        public static string ToLowerCamelCase(this string className) =>
            char.ToLowerInvariant(className[0]) + className.Substring(1);

        public static string JoinWithComma(this string[] strs) => string.Join(", ", strs);
    }
}