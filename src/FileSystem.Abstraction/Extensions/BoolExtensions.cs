namespace FileSystem.Abstraction.Extensions
{
    public static class BoolExtensions
    {
        public static bool IsFalse(this bool source)
        {
            return !source;
        }
    }
}
