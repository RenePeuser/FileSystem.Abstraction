using System.IO;
using FileSystem.Abstraction.ArgumentCheck;

namespace FileSystem.Abstraction.Services
{
    public class DirectoryService : IDirectoryService
    {
        public IDirectoryInfo GetCurrentDirectory()
        {
            return GetDirectoryInfo(Directory.GetCurrentDirectory());
        }

        public IDirectoryInfo SetCurrentDirectoryInfo(IFileInfo fileInfo)
        {
            Throw.IfNull(() => fileInfo);

            Directory.SetCurrentDirectory(fileInfo.Directory.FullName);

            return fileInfo.Directory;
        }

        public IDirectoryInfo SetCurrentDirectoryInfo(IDirectoryInfo directoryInfo)
        {
            Throw.IfNull(() => directoryInfo);

            Directory.SetCurrentDirectory(directoryInfo.FullName);

            return directoryInfo;
        }

        public IDirectoryInfo GetDirectoryInfo(string path)
        {
            Throw.IfNullOrWhiteSpace(() => path);

            var directoryInfo = new System.IO.DirectoryInfo(path);
            var result = new DirectoryInfo(directoryInfo);
            return result;
        }

        public IDirectoryInfo GetTempDirectory()
        {
            return GetDirectoryInfo(Path.GetTempPath());
        }

        public string Combine(params string[] paths)
        {
            Throw.IfNull(() => paths);
            Throw.IfAnyItemIsNullOrWhitespace(() => paths);

            return Path.Combine(paths);
        }

        public IDirectoryInfo CreateDirectory(string directoryName)
        {
            Throw.IfNullOrWhiteSpace(() => directoryName);

            var directoryInfo = Directory.CreateDirectory(directoryName);
            return new DirectoryInfo(directoryInfo);
        }
    }
}
