using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace FileSystem.Abstraction.ArgumentCheck
{
    internal static class FuncFieldInfoExtractor
    {
        [DebuggerHidden]
        internal static FieldInfo GetFieldInfo<T>(this Func<T> func)
        {
            Throw.IfNull(() => func);

            var targetType = func.Target.GetType();
            var fields = targetType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var rebuildIlMethods = func.Rebuild(fields);
            var checkArgumentFieldInfo = rebuildIlMethods.First(m => m.MethodInfo == func.Method);
            return checkArgumentFieldInfo.ReturnValue;
        }

        [DebuggerHidden]
        internal static string GetParameterName<T>(this Func<T> func)
        {
            Throw.IfNull(() => func);

            var result = GetFieldInfo(func);
            return result.Name;
        }
    }
}
