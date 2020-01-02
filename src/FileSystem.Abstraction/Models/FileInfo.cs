using System.Collections.Generic;
using System.IO;
using System.Text;
using FileSystem.Abstraction.ArgumentCheck;

namespace FileSystem.Abstraction
{
    public class FileInfo : FileSystemInfo<System.IO.FileInfo>, IFileInfo
    {
        public FileInfo(System.IO.FileInfo info)
            : base(info)
        {
        }

        public IDirectoryInfo Directory
        {
            get
            {
                var directoryInfo = new DirectoryInfo(Instance.Directory);
                return directoryInfo;
            }
        }

        public string DirectoryName => Instance.DirectoryName;

        public bool IsReadOnly
        {
            get => Instance.IsReadOnly;

            set => Instance.IsReadOnly = value;
        }

        public long Length => Instance.Length;

        public StreamWriter AppendText()
        {
            var stream = Instance.AppendText();
            return stream;
        }

        public IFileInfo CopyTo(string destinationFileName, bool overwrite)
        {
            Throw.IfNullOrWhiteSpace(() => destinationFileName);

            var copiedFileInfo = Instance.CopyTo(destinationFileName, overwrite);
            var result = new FileInfo(copiedFileInfo);
            return result;
        }

        public Stream Create()
        {
            var stream = Instance.Create();
            return stream;
        }

        public StreamWriter CreateText()
        {
            var stream = Instance.CreateText();
            return stream;
        }

        public void MoveTo(string destinationFileName)
        {
            Throw.IfNullOrWhiteSpace(() => destinationFileName);

            Instance.MoveTo(destinationFileName);
        }

        public Stream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return Instance.Open(mode, access, share);
        }

        public Stream OpenRead()
        {
            var stream = Instance.OpenRead();
            return stream;
        }

        public StreamReader OpenText()
        {
            var stream = Instance.OpenText();
            return stream;
        }

        public Stream OpenWrite()
        {
            var stream = Instance.OpenWrite();
            return stream;
        }

        public void AppendAllLines(IEnumerable<string> contents)
        {
            Throw.IfNull(() => contents);

            File.AppendAllLines(Instance.FullName, contents);
        }

        public void AppendAllLines(IEnumerable<string> contents, Encoding encoding)
        {
            Throw.IfNull(() => contents);

            File.AppendAllLines(Instance.FullName, contents, encoding);
        }

        public void AppendAllText(string contents)
        {
            Throw.IfNull(() => contents);

            File.AppendAllText(Instance.FullName, contents);
        }

        public void AppendAllText(string contents, Encoding encoding)
        {
            Throw.IfNull(() => contents);

            File.AppendAllText(Instance.FullName, contents, encoding);
        }

        public byte[] ReadAllBytes()
        {
            return File.ReadAllBytes(Instance.FullName);
        }

        public string[] ReadAllLines()
        {
            return File.ReadAllLines(Instance.FullName);
        }

        public string[] ReadAllLines(Encoding encoding)
        {
            return File.ReadAllLines(Instance.FullName, encoding);
        }

        public string ReadAllText()
        {
            return File.ReadAllText(Instance.FullName);
        }

        public string ReadAllText(Encoding encoding)
        {
            return File.ReadAllText(Instance.FullName, encoding);
        }

        public void WriteAllBytes(byte[] contents)
        {
            Throw.IfNull(() => contents);

            File.WriteAllBytes(Instance.FullName, contents);
        }

        public void WriteAllLines(IEnumerable<string> contents)
        {
            Throw.IfNull(() => contents);

            File.WriteAllLines(Instance.FullName, contents);
        }

        public void WriteAllLines(IEnumerable<string> contents, Encoding encoding)
        {
            Throw.IfNull(() => contents);

            File.WriteAllLines(Instance.FullName, contents, encoding);
        }

        public void WriteAllText(string contents)
        {
            Throw.IfNull(() => contents);

            File.WriteAllText(Instance.FullName, contents);
        }

        public void WriteAllText(string contents, Encoding encoding)
        {
            Throw.IfNull(() => contents);

            File.WriteAllText(Instance.FullName, contents, encoding);
        }
    }
}
