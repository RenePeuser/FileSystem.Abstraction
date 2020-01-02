using System;
using FileSystem.Abstraction.ArgumentCheck;

namespace FileSystem.Abstraction.Extensions
{
    public static class ComparableExtensions
    {
        public static bool IsLessThan<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) < 0;
        }

        public static bool IsLessOrEqual<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return !source.IsGreaterThan(target);
        }

        public static bool IsGreaterThan<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return source.CompareTo(target) > 0;
        }

        public static bool IsGreaterOrEqual<T>(this T source, T target)
            where T : IComparable
        {
            Throw.IfNull<object>(() => source);
            Throw.IfNull<object>(() => target);

            return !source.IsLessThan(target);
        }
    }
}
