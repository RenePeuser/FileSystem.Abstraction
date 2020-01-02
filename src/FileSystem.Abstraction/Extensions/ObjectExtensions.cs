namespace FileSystem.Abstraction.Extensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object source)
        {
            var result = default(T);

            if (source is T)
            {
                result = (T)source;
            }

            return result;
        }

        public static T Cast<T>(this object source) where T : class
        {
            return (T)source;
        }

        public static bool Is<T>(this object source)
        {
            return source is T;
        }

        public static bool IsNot<T>(this object source)
        {
            return Is<T>(source).IsFalse();
        }

        public static bool IsNotNull(this object source)
        {
            return !source.EqualsTo(null);
        }

        public static bool IsNull(this object source)
        {
            return source.EqualsTo(null);
        }
    }
}
