namespace FileSystem.Abstraction.Services
{
    public interface IFileService
    {
        IFileInfo GetFileInfo(string path);
    }
}
