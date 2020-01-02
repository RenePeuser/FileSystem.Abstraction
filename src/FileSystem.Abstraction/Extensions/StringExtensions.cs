using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public static bool ContainsNotAnyOf(this string source, params string[] notContainStrings)
        {
            return !notContainStrings.Any(source.Contains);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNotNullOrEmpty(this string source)
        {
            return !source.IsNullOrEmpty();
        }

        public static bool IsEmpty(this string source)
        {
            return source == string.Empty;
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        public static bool IsNotNullOrWhiteSpace(this string source)
        {
            return source.IsNullOrWhiteSpace().IsFalse();
        }

        public static bool IsValid(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            return true;
        }

        public static bool IsNotValid(this string source)
        {
            return !source.IsValid();
        }

        public static DateTime ToDateTime(this string source, DateTimeFormatInfo dateTimeFormatInfo, DateTimeStyles dateTimeStyles)
        {
            Throw.IfNullOrWhiteSpace(() => source);

            DateTime dateTime;
            var result = DateTime.TryParse(source, dateTimeFormatInfo, dateTimeStyles, out dateTime);

            if (!result)
            {
                throw new InvalidOperationException(string.Format(dateTimeFormatInfo, "Can not parse string: {0} to type DateTime", source));
            }

            return dateTime;
        }

        public static IEnumerable<string> Split(this string value, int blockLength)
        {
            if (value.IsNotValid())
            {
                yield break;
            }

            if (blockLength.IsLessOrEqual(default))
            {
                throw new ArgumentException("The length of a block must not be 0 or smaller");
            }

            var expectedBlocks = value.Length.DivideBy(blockLength.ToDouble()).Ceiling();

            for (var i = 0; i < expectedBlocks; i++)
            {
                yield return value.SubstringUpTo(i.MultiplyBy(blockLength), blockLength);
            }
        }

        public static string SubstringUpTo(this string value, int startIndex, int length)
        {
            Throw.IfNullOrWhiteSpace(() => value);

            var stringLength = value.Length;
            var maxLength = startIndex.Plus(length).IsLessOrEqual(stringLength) ? length : stringLength - startIndex;

            return value.Substring(startIndex, maxLength);
        }
    }
}
