using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace FileSystem.Abstraction.ArgumentCheck
{
    internal static class Throw
    {
        [DebuggerHidden]
        public static void IfAnyItemIsNullOrWhitespace(Func<IEnumerable<string>> argumentFunc)
        {
            IfNull(() => argumentFunc);
            IfNull(argumentFunc);

            if (!argumentFunc().IsAnyItemNullOrWhitespace())
            {
                return;
            }

            ThrowIsAnyItemNullOrWhiteSpaceException(argumentFunc, arg => arg.Is<IEnumerable<string>>() && arg.Cast<IEnumerable<string>>().IsAnyItemNullOrWhitespace());
        }


        [DebuggerHidden]
        internal static void IfNull<T>(Func<T> argumentFunc) where T : class
        {
            if (argumentFunc == null)
            {
                throw new ArgumentNullException(nameof(argumentFunc));
            }

            IfNullInternal(argumentFunc);
        }

        [DebuggerHidden]
        internal static void IfNullOrWhiteSpace(Func<string> argumentFunc)
        {
            IfNull(() => argumentFunc);
            IfNullInternal(argumentFunc);
            IfWhiteSpace(argumentFunc);
        }

        [DebuggerHidden]
        private static void IfNullInternal<T>(Func<T> argumentFunc)
            where T : class
        {
            if (argumentFunc() != null)
            {
                return;
            }

            throw new ArgumentNullException(argumentFunc.GetParameterName(arg => arg == null));
        }

        [DebuggerHidden]
        private static void IfWhiteSpace(Func<string> argument)
        {
            if (!argument().IsNullOrWhiteSpace())
            {
                return;
            }

            throw new ArgumentException("The string must not be a whitespace.", argument.GetParameterName(arg => arg.Is<string>() && arg.Cast<string>().IsNullOrWhiteSpace()));
        }

        [DebuggerHidden]
        private static void ThrowLessThanException<T>(Func<T> argumentFunc, T limit, Func<object, bool> predicate) where T : IComparable
        {
            throw new ArgumentOutOfRangeException(argumentFunc.GetParameterName(predicate), string.Format(CultureInfo.InvariantCulture, "Value: '{0}' must not be less than: '{1}'", argumentFunc(), limit));
        }

        [DebuggerHidden]
        private static void ThrowIsAnyItemNullOrWhiteSpaceException(
            Func<IEnumerable<string>> argumentFunc,
            Func<object, bool> predicate)
        {
            throw new ArgumentException($"At least one string in the enumeration '{string.Join(",", argumentFunc())}' was null or a whitespace.", argumentFunc.GetParameterName(predicate));
        }
    }
}
