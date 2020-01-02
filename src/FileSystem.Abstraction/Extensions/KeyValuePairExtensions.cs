using System;
using System.Collections.Generic;
using System.Globalization;
using FileSystem.Abstraction.ArgumentCheck;

namespace FileSystem.Abstraction.Extensions
{
    public static class KeyValuePairExtensions
    {
        public static string ToString<T>(this KeyValuePair<string, Func<T, object>> compiledExpression, T argument)
        {
            Throw.IfNull<object>(() => argument);

            var result = string.Format(CultureInfo.InvariantCulture, "{0}[{1}] ", compiledExpression.Key, compiledExpression.Value.Invoke(argument));

            return result;
        }
    }
}
