using System;
using System.IO;
using FileSystem.Abstraction.ArgumentCheck;
using FileSystem.Abstraction.Extensions;

namespace FileSystem.Abstraction
{
    public abstract class FileSystemInfo<T> : IFileSystemInfo
        where T : FileSystemInfo
    {
        public FileSystemInfo(T info)
        {
            Throw.IfNull(() => info);
            Instance = info;
        }

        protected T Instance { get; }

        public FileAttributes Attributes
        {
            get => Instance.Attributes;

            set => Instance.Attributes = value;
        }

        public DateTime CreationTime
        {
            get => Instance.CreationTime;

            set => Instance.CreationTime = value;
        }

        public bool Exists => Instance.Exists;

        public bool NotExists => Exists.IsFalse();

        public string Extension => Instance.Extension;

        public string FullName => Instance.FullName;

        public DateTime LastAccessTime
        {
            get => Instance.LastAccessTime;

            set => Instance.LastAccessTime = value;
        }

        public DateTime LastWriteTime
        {
            get => Instance.LastWriteTime;

            set => Instance.LastWriteTime = value;
        }

        public string Name => Instance.Name;

        public void Delete()
        {
            Instance.Delete();
            Instance.Refresh();
        }

        public void Refresh()
        {
            Instance.Refresh();
        }
    }
}
