namespace FileSystem.Abstraction
{
    public interface IDirectoryService
    {
        IDirectoryInfo GetDirectoryInfo(string path);
        IDirectoryInfo GetCurrentDirectory();
        IDirectoryInfo GetTempDirectory();

        string Combine(params string[] paths);

        IDirectoryInfo CreateDirectory(string directoryName);
        IDirectoryInfo SetCurrentDirectoryInfo(IFileInfo fileInfo);
        IDirectoryInfo SetCurrentDirectoryInfo(IDirectoryInfo directoryInfo);
    }
}
