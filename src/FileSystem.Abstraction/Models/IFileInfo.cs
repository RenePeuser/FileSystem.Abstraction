using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileSystem.Abstraction
{
    public interface IFileInfo : IFileSystemInfo
    {
        IDirectoryInfo Directory { get; }

        string DirectoryName { get; }

        bool IsReadOnly { get; set; }

        long Length { get; }

        string NameWithoutExtension { get; }

        string ExtensionName { get; }

        StreamWriter AppendText();

        IFileInfo CopyTo(string destFileName, bool overwrite);

        Stream Create();

        StreamWriter CreateText();

        void MoveTo(string destFileName);

        Stream Open(FileMode mode, FileAccess access, FileShare share);

        Stream OpenRead();

        StreamReader OpenText();

        Stream OpenWrite();

        void AppendAllLines(IEnumerable<string> contents);

        void AppendAllLines(IEnumerable<string> contents, Encoding encoding);

        void AppendAllText(string contents);

        void AppendAllText(string contents, Encoding encoding);

        byte[] ReadAllBytes();

        string[] ReadAllLines();

        string[] ReadAllLines(Encoding encoding);

        string ReadAllText();

        string ReadAllText(Encoding encoding);

        void WriteAllBytes(byte[] contents);

        void WriteAllLines(IEnumerable<string> contents);

        void WriteAllLines(IEnumerable<string> contents, Encoding encoding);

        void WriteAllText(string contents);

        void WriteAllText(string contents, Encoding encoding);
    }
}
