using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace FileSystem.Abstraction.ArgumentCheck
{
    //void Test(int vegeta, int songoku)        => <> c__DisplayClass10_0
    //{
    //    Throw.IfLessThan(() => vegeta, 1);    => <>c__DisplayClass10_0'::vegeta     => // end of method '<>c__DisplayClass10_0'::'<Should_Throw_Correct_Argument>b__2'  __x => increasing number (2)
    //    Throw.IfLessThan(() => songoku, 1);   => <>c__DisplayClass10_0'::songoku    => // end of method '<>c__DisplayClass10_0'::'<Should_Throw_Correct_Argument>b__3'  __x => increasing number (3)
    //}

    // This is only a helper class for trying reconstruct what il is generation, ONLY for our case of argument checking !!!!!
    internal static class ILMethodToFieldRebuilder
    {
        private const string Sample = @"

Simple usage:

public Class(IService service)
{
    Throw.IfNull(() => service);
}

Variables which exists on stack:

public Class(IService service)
{
    Throw.IfNull(() => service);

    var result = service.GetSomething();
    Throw.IfNull(() => result);
}
";

        [DebuggerHidden]
        internal static IEnumerable<ILReconstructedMethod> Rebuild<T>(this Func<T> func, IEnumerable<FieldInfo> fieldInfos, Func<object, bool> predicate)
        {
            Throw.IfNull(() => func);
            Throw.IfNull(() => fieldInfos);

            var method = func.Method;
            var typeInfo = method.DeclaringType.Cast<TypeInfo>();
            var typeInfoDeclaredMethods = typeInfo.DeclaredMethods.ToList();
            var declaredMethods = typeInfoDeclaredMethods.Where(m => fieldInfos.Any(f => m.ReturnType.IsAssignableFrom(f.FieldType))).ToList();
            var filteredFieldInfos = fieldInfos.Where(f => declaredMethods.Any(m => m.ReturnType.IsAssignableFrom(f.FieldType))).ToList();

            // if we have generated methods which match not the return type of our parameters then we are not in our scope any more.
            var methodsWhichNotMatchFieldType = !declaredMethods.Any() || !filteredFieldInfos.Any() || declaredMethods.Any(m => fieldInfos.All(f => !m.ReturnType.IsAssignableFrom(f.FieldType)));
            if (methodsWhichNotMatchFieldType)
            {
                throw new InvalidOperationException($"Please check, that you only do argument checking with the 'Throw' helpers in the scope of an constructor or method. It will not work for outer scope stuff. Sample:{Environment.NewLine}{Sample}");
            }

            // Easiest case 1 argument 1 check (Multiple arguments)
            if (declaredMethods.Count == filteredFieldInfos.Count)
            {
                var rebuildIlMethods = FetchOneArgumentOneCheck(declaredMethods, filteredFieldInfos);
                return rebuildIlMethods;
            }

            // 
            var result = FetchMultiCheckForOneArgument(func, declaredMethods, filteredFieldInfos, predicate).ToList();
            return result;
        }

        // Simple case works great !
        // public Class(object a, object b, object c)
        // {
        //     Throw.IfNull(() => a);
        //     Throw.IfNull(() => b);
        //     Throw.IfNull(() => c);
        // }
        [DebuggerHidden]
        private static IEnumerable<ILReconstructedMethod> FetchOneArgumentOneCheck(IEnumerable<MethodInfo> declaredMethods, IEnumerable<FieldInfo> fieldInfos)
        {
            var orderedMethods = declaredMethods.Select(m => new { m, index = m.Name.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries).Last() }).OrderBy(x => x.index).Select(x => x.m).ToList();
            var ilMethods = fieldInfos.Select((field, index) => new ILReconstructedMethod(orderedMethods[index], field));
            return ilMethods;
        }


        // Worst case works not perfect now.
        // public Class(string a, object b, object c)
        // {
        //     Throw.IfNull(() => a);
        //     Throw.IfWhitespace(() => a);
        //     
        //     Throw.IfNull(() => b);
        //     Throw.IfNull(() => c);
        // }
        [DebuggerHidden]
        private static IEnumerable<ILReconstructedMethod> FetchMultiCheckForOneArgument<T>(this Func<T> func, IEnumerable<MethodInfo> declaredMethods, IEnumerable<FieldInfo> fieldInfos, Func<object, bool> predicate)
        {
            //var orderedMethods = declaredMethods.Select(m => new { m, index = m.Name.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries).Last() }).OrderBy(x => x.index).Select(x => x.m).ToList();

            yield return new ILReconstructedMethod(func.Method, fieldInfos.FirstOrDefault(f => predicate(f.GetValue(func.Target))));
        }
    }
}