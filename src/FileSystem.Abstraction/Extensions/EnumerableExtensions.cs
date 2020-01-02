using System;
using System.Collections.Generic;
using System.Linq;
using FileSystem.Abstraction.ArgumentCheck;

namespace FileSystem.Abstraction.Extensions
{
    public static class EnumerableExtensions
    {
        internal static bool IsAnyItem<T>(this IEnumerable<T> source, Predicate<T> check)
        {
            Throw.IfNull(() => source);

            return source.Any(item => check(item));
        }

        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            Throw.IfNull(() => source);
            Throw.IfNull(() => action);

            var sourceList = source.ToList();
            sourceList.ForEach(action);
        }
    }
}
