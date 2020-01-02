using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystem.Abstraction.ArgumentCheck;

namespace FileSystem.Abstraction
{
    public class DirectoryInfo : FileSystemInfo<System.IO.DirectoryInfo>, IDirectoryInfo
    {
        public DirectoryInfo(System.IO.DirectoryInfo info)
            : base(info)
        {
        }

        public IDirectoryInfo Parent => CreateDirectoryInfo(Instance.Parent);

        public IDirectoryInfo Root => CreateDirectoryInfo(Instance.Root);

        public void Create()
        {
            Instance.Create();
        }

        public IDirectoryInfo CreateSubDirectory(string path)
        {
            Throw.IfNullOrWhiteSpace(() => path);

            var subDirectoryInfo = Instance.CreateSubdirectory(path);
            var result = CreateDirectoryInfo(subDirectoryInfo);
            return result;
        }

        public void Delete(bool recursive)
        {
            Instance.Delete(recursive);
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories()
        {
            return Instance.EnumerateDirectories().Select(CreateDirectoryInfo);
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories(string searchPattern)
        {
            Throw.IfNullOrWhiteSpace(() => searchPattern);

            return Instance.EnumerateDirectories(searchPattern).Select(CreateDirectoryInfo);
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
        {
            Throw.IfNullOrWhiteSpace(() => searchPattern);

            return Instance.EnumerateDirectories(searchPattern, searchOption).Select(CreateDirectoryInfo);
        }

        public IEnumerable<IFileInfo> EnumerateFiles()
        {
            return Instance.EnumerateFiles().Select(CreateFileInfo);
        }

        public IEnumerable<IFileInfo> EnumerateFiles(string searchPattern)
        {
            Throw.IfNullOrWhiteSpace(() => searchPattern);

            return Instance.EnumerateFiles(searchPattern).Select(CreateFileInfo);
        }

        public IEnumerable<IFileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
        {
            Throw.IfNullOrWhiteSpace(() => searchPattern);

            return Instance.EnumerateFiles(searchPattern, searchOption).Select(CreateFileInfo);
        }

        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos()
        {
            return Instance.EnumerateFileSystemInfos().Select(CreateFileSystemInfo);
        }

        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(string searchPattern)
        {
            Throw.IfNullOrWhiteSpace(() => searchPattern);

            return Instance.EnumerateFileSystemInfos(searchPattern).Select(CreateFileSystemInfo);
        }

        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(string searchPattern, SearchOption searchOption)
        {
            Throw.IfNullOrWhiteSpace(() => searchPattern);

            return Instance.EnumerateFileSystemInfos(searchPattern, searchOption).Select(CreateFileSystemInfo);
        }

        public void MoveTo(string destinationDirectory)
        {
            Throw.IfNullOrWhiteSpace(() => destinationDirectory);

            Instance.MoveTo(destinationDirectory);
        }

        private static DirectoryInfo CreateDirectoryInfo(System.IO.DirectoryInfo info)
        {
            var directoryInfo = new DirectoryInfo(info);
            return directoryInfo;
        }

        private static FileInfo CreateFileInfo(System.IO.FileInfo info)
        {
            var fileInfo = new FileInfo(info);
            return fileInfo;
        }

        private static IFileSystemInfo CreateFileSystemInfo(FileSystemInfo info)
        {
            if (info is System.IO.FileInfo)
            {
                return CreateFileInfo((System.IO.FileInfo) info);
            }

            if (info is System.IO.DirectoryInfo)
            {
                return CreateDirectoryInfo((System.IO.DirectoryInfo) info);
            }

            throw new InvalidOperationException();
        }
    }
}
