using System.Reflection;

namespace FileSystem.Abstraction.ArgumentCheck
{
    internal class ILReconstructedMethod
    {
        public ILReconstructedMethod(MethodInfo methodInfo, FieldInfo returnValue)
        {
            Throw.IfNull(() => methodInfo);
            Throw.IfNull(() => returnValue);

            MethodInfo = methodInfo;
            ReturnValue = returnValue;
        }

        internal MethodInfo MethodInfo { get; }

        internal FieldInfo ReturnValue { get; }
    }
}