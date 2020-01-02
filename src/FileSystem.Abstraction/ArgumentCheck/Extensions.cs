using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FileSystem.Abstraction.Extensions;

namespace FileSystem.Abstraction.ArgumentCheck
{
    public static class ArgumentCheckExtensions
    {
        [DebuggerHidden]
        internal static bool IsAnyItemNullOrWhitespace(this IEnumerable<string> source)
        {
            Throw.IfNull(() => source);

            return source.IsAnyItem(item => item.IsNullOrWhiteSpace());
        }

        [DebuggerHidden]
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        [DebuggerHidden]
        public static T Cast<T>(this object source) where T : class
        {
            return (T)source;
        }

        [DebuggerHidden]
        public static bool Is<T>(this object source)
        {
            return source is T;
        }

        [DebuggerHidden]
        internal static bool IsAnyItem<T>(this IEnumerable<T> source, Predicate<T> check)
        {
            Throw.IfNull(() => source);

            return source.Any(item => check(item));
        }
    }
}
