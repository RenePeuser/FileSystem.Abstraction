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
        internal static IEnumerable<ILReconstructedMethod> Rebuild<T>(this Func<T> func, IEnumerable<FieldInfo> fieldInfos)
        {
            Throw.IfNull(() => func);
            Throw.IfNull(() => fieldInfos);

            var method = func.Method;
            var typeInfo = method.DeclaringType.Cast<TypeInfo>();

            var typeInfoDeclaredMethods = typeInfo.DeclaredMethods;

            var declaredMethods = typeInfoDeclaredMethods.Where(m => fieldInfos.Any(f => m.ReturnParameter.ParameterType.IsAssignableFrom(f.FieldType))).ToList();

            var methodsWhichNotMatchFieldType = typeInfoDeclaredMethods.Except(declaredMethods).ToList();
            if (methodsWhichNotMatchFieldType.Any())
            {
                throw new InvalidOperationException($"Please check, that you only do argument checking with the 'Throw' helpers in the scope of an constructor or method. It will not work for outer scope stuff. Sample:{Environment.NewLine}{Sample}");
            }

            if (declaredMethods.Count == fieldInfos.Count())
            {
                var rebuildIlMethods = FetchOneArgumentOneCheck(declaredMethods, fieldInfos);
                return rebuildIlMethods;
            }

            var result = FetchMultiCheckForOneArgument(declaredMethods, fieldInfos).ToList();
            return result;
        }

        [DebuggerHidden]
        private static IEnumerable<ILReconstructedMethod> FetchOneArgumentOneCheck(IEnumerable<MethodInfo> declaredMethods, IEnumerable<FieldInfo> fieldInfos)
        {
            var orderedMethods = declaredMethods.Select(m => new { m, index = m.Name.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries).Last() }).OrderBy(x => x.index).Select(x => x.m).ToList();
            var ilMethods = fieldInfos.Select((field, index) => new ILReconstructedMethod(orderedMethods[index], field));
            return ilMethods;
        }

        [DebuggerHidden]
        private static IEnumerable<ILReconstructedMethod> FetchMultiCheckForOneArgument(IEnumerable<MethodInfo> declaredMethods, IEnumerable<FieldInfo> fieldInfos)
        {
            var orderedMethods = declaredMethods.Select(m => new { m, index = m.Name.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries).Last() }).OrderBy(x => x.index).Select(x => x.m).ToList();
            var counetr = declaredMethods.Count() / fieldInfos.Count();

            int count = 0;
            int index = 0;
            foreach (var orderedMethod in orderedMethods)
            {
                if (count == counetr)
                {
                    index++;
                }
                var fieldForMethod = fieldInfos.ElementAt(index);
                count++;
                yield return new ILReconstructedMethod(orderedMethod, fieldForMethod);
            }
        }
    }
}