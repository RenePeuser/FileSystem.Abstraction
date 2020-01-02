using System;

namespace FileSystem.Abstraction.Extensions
{
    public static class IntegerExtensions
    {
        public static double DivideBy(this int value, double divisor)
        {
            if (divisor.IsZero())
            {
                throw new ArgumentException("Division by 0 is not allowed");
            }

            if (divisor.IsNan())
            {
                throw new ArgumentException("Divisor is not a number");
            }

            return value / divisor;
        }

        public static int MultiplyBy(this int value, int multiplier)
        {
            return value * multiplier;
        }

        public static int Plus(this int value, int addend)
        {
            return value + addend;
        }

        public static double ToDouble(this int value)
        {
            return value;
        }
    }
}
