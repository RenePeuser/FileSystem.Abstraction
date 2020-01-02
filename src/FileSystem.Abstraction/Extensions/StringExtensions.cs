using System.Collections.Generic;
using FileSystem.Abstraction.ArgumentCheck;

namespace FileSystem.Abstraction.Extensions
{
    public static class StringExtensions
    {
        internal static bool IsAnyItemNullOrWhitespace(this IEnumerable<string> source)
        {
            Throw.IfNull(() => source);

            return source.IsAnyItem(item => item.IsNullOrWhiteSpace());
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }
    }
}
