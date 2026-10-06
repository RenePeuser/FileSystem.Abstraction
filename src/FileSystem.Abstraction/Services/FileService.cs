using FileSystem.Abstraction.ArgumentCheck;

namespace FileSystem.Abstraction
{
    public class FileService : IFileService
    {
        public IFileInfo GetFileInfo(string path)
        {
            Throw.IfNullOrWhiteSpace(() => path);

            var systemFileInfo = new System.IO.FileInfo(path);
            var fileInfo = new FileInfo(systemFileInfo);
            return fileInfo;
        }
    }
}
