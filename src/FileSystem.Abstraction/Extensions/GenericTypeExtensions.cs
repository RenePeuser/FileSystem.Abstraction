using System;
using System.Collections.Generic;

namespace FileSystem.Abstraction.Extensions
{
    public static class GenericTypeExtensions
    {
        private const double DEFAULT_DOUBLE_TOLERANCE = 0.000001;

        public static bool EqualsTo<T>(this T source, T target)
        {
            return EqualityComparer<T>.Default.Equals(source, target);
        }

        public static bool EqualsTo(this double source, double target)
        {
            return EqualsTo(source, target, DEFAULT_DOUBLE_TOLERANCE);
        }

        public static bool NotEqualsTo(this double source, double target)
        {
            return NotEqualsTo(source, target, DEFAULT_DOUBLE_TOLERANCE);
        }

        public static bool EqualsTo(this double source, double target, double tolerance)
        {
            return Math.Abs(source - target).IsLessThan(tolerance);
        }

        public static bool NotEqualsTo(this double source, double target, double tolerance)
        {
            return !source.EqualsTo(target, tolerance);
        }

        public static bool NotEqualsTo<T>(this T source, T target)
        {
            return !source.EqualsTo(target);
        }
    }
}
