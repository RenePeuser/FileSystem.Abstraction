namespace FileSystem.Abstraction
{
    public interface IFileService
    {
        IFileInfo GetFileInfo(string path);
    }
}
