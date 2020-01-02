using System;

namespace FileSystem.Abstraction.Extensions
{
    public static class DoubleExtensions
    {
        public static bool IsNan(this double value)
        {
            return double.IsNaN(value);
        }

        public static bool IsZero(this double source)
        {
            var result = source.EqualsTo(default);

            return result;
        }

        public static int Ceiling(this double value)
        {
            return (int) Math.Ceiling(value);
        }
    }
}
