using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoryInfo = FileSystem.Abstraction.DirectoryInfo;

namespace FileSystem.Abstraction.Test.FileSystem
{
    [TestClass]
    public class DirectoryInfoTest
    {
        private static string _subDirectory;
        private static string _file;
        private static System.IO.DirectoryInfo _systemDirectoryInfo;
        private DirectoryInfo _directoryInfo;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _systemDirectoryInfo = CreateTempDirectoryInfo();
            Directory.CreateDirectory(_systemDirectoryInfo.FullName);

            _subDirectory = Path.Combine(_systemDirectoryInfo.FullName, Guid.NewGuid().ToString());
            Directory.CreateDirectory(_subDirectory);

            _file = Path.Combine(_systemDirectoryInfo.FullName, Guid.NewGuid() + ".txt");
            File.WriteAllText(_file, string.Empty);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if (_systemDirectoryInfo != null && _systemDirectoryInfo.Exists)
            {
                _systemDirectoryInfo.Delete(true);
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            _directoryInfo = new DirectoryInfo(_systemDirectoryInfo);
        }

        [TestMethod]
        public void WhenDirectoryInfoIsCreatedWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DirectoryInfo(null), "info");
        }

        [TestMethod]
        public void WhenDirectoryInfoIsCreated_ThenParentIsSet()
        {
            Assert.AreEqual(_systemDirectoryInfo.Parent.FullName, _directoryInfo.Parent.FullName);
        }

        [TestMethod]
        public void WhenDirectoryInfoIsCreated_ThenRootIsSet()
        {
            Assert.AreEqual(_systemDirectoryInfo.Root.FullName, _directoryInfo.Root.FullName);
        }

        [TestMethod]
        public void WhenCreateIsCalled_ThenDirectoryIsCreated()
        {
            var directory = CreateTempDirectoryInfo();
            var testObject = new DirectoryInfo(directory);
            Assert.IsFalse(testObject.Exists);

            testObject.Create();
            testObject.Refresh();

            Assert.IsTrue(testObject.Exists);
            testObject.Delete(false);
        }

        [TestMethod]
        public void WhenMoveToIsCalledWithInvalidArgument_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _directoryInfo.MoveTo(""), "destinationDirectory");
        }

        [TestMethod]
        public void WhenMoveToIsCalled_ThenDirectoryIsMoved()
        {
            var directory = CreateTempDirectoryInfo();
            var targetDirectory = CreateTempDirectoryInfo();
            var testObject = new DirectoryInfo(directory);
            Directory.CreateDirectory(testObject.FullName);

            testObject.MoveTo(targetDirectory.FullName);
            testObject.Refresh();

            Assert.IsTrue(testObject.Exists);
            Assert.AreEqual(targetDirectory.FullName.TrimEnd('\\'), testObject.FullName.TrimEnd('\\'));
            testObject.Delete(false);
        }

        [TestMethod]
        public void WhenDeleteIsCalled_ThenDirectoryIsDeleted()
        {
            var directory = CreateTempDirectoryInfo();
            var testObject = new DirectoryInfo(directory);
            testObject.Create();

            testObject.Delete();

            Assert.IsFalse(testObject.Exists);
        }

        [TestMethod]
        public void WhenEnumerateFileSystemInfosWithPatternIsCalledWithInvalidArgument_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _directoryInfo.EnumerateFileSystemInfos(""), "destinationDirectory");
        }

        [TestMethod]
        public void WhenEnumerateFileSystemInfosIsCalled_ThenSubdirectoryAndFileAreReturned()
        {
            var result = _directoryInfo.EnumerateFileSystemInfos().ToList();

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(f => f is IFileInfo && f.FullName == _file));
            Assert.IsTrue(result.Any(f => f is IDirectoryInfo && f.FullName == _subDirectory));
        }

        [TestMethod]
        public void WhenEnumerateFileSystemInfosWithOptionsIsCalledWithInvalidArgument_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _directoryInfo.EnumerateFileSystemInfos("", SearchOption.AllDirectories), "destinationDirectory");
        }

        [TestMethod]
        public void WhenEnumerateFileSystemInfosWithMaskIsCalled_ThenFileIsReturned()
        {
            var result = _directoryInfo.EnumerateFileSystemInfos("*.txt").ToList();

            Assert.AreEqual(1, result.Count);
            Assert.IsInstanceOfType(result[0], typeof(IFileInfo));
            Assert.AreEqual(_file, result[0].FullName);
        }

        [TestMethod]
        public void WhenEnumerateFileSystemInfosWithMaskAndOptionsIsCalled_ThenFileIsReturned()
        {
            var result = _directoryInfo.EnumerateFileSystemInfos("*.txt", SearchOption.AllDirectories).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.IsInstanceOfType(result[0], typeof(IFileInfo));
            Assert.AreEqual(_file, result[0].FullName);
        }

        [TestMethod]
        public void WhenEnumerateFilesWithPatternIsCalledWithInvalidArgument_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _directoryInfo.EnumerateFiles(""), "searchPattern");
        }

        [TestMethod]
        public void WhenEnumerateFilesIsCalled_ThenFileIsReturned()
        {
            var result = _directoryInfo.EnumerateFiles().ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_file, result[0].FullName);
        }

        [TestMethod]
        public void WhenEnumerateFilesWithOptionsIsCalledWithInvalidArgument_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _directoryInfo.EnumerateFiles("", SearchOption.AllDirectories), "searchPattern");
        }

        [TestMethod]
        public void WhenEnumerateFilesWithMaskIsCalled_ThenFileIsReturned()
        {
            var result = _directoryInfo.EnumerateFiles("*.txt").ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_file, result[0].FullName);
        }

        [TestMethod]
        public void WhenEnumerateFilesWithMaskAndOptionsIsCalled_ThenFileIsReturned()
        {
            var result = _directoryInfo.EnumerateFiles("*.txt", SearchOption.AllDirectories).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_file, result[0].FullName);
        }

        [TestMethod]
        public void WhenEnumerateDirectoriesIsCalled_ThenSubdirectoryIsReturned()
        {
            var result = _directoryInfo.EnumerateDirectories().ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_subDirectory, result[0].FullName);
        }

        [TestMethod]
        public void WhenEnumerateDirectoriesWithPatternIsCalledWithInvalidArgument_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _directoryInfo.EnumerateDirectories(""), "searchPattern");
        }

        [TestMethod]
        public void WhenEnumerateDirectoriesWithMaskIsCalled_ThenSubdirectoryIsReturned()
        {
            var result = _directoryInfo.EnumerateDirectories("*").ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_subDirectory, result[0].FullName);
        }

        [TestMethod]
        public void WhenEnumerateDirectoriesWithOptionsIsCalledWithInvalidArgument_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _directoryInfo.EnumerateDirectories("", SearchOption.AllDirectories));
        }

        [TestMethod]
        public void WhenEnumerateDirectoriesWithMaskAndOptionsIsCalled_ThenSubdirectoryIsReturned()
        {
            var result = _directoryInfo.EnumerateDirectories("*", SearchOption.AllDirectories).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_subDirectory, result[0].FullName);
        }

        [TestMethod]
        public void WhenCreateSubdirectoryIsCalledWithInvalidArgument_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _directoryInfo.CreateSubdirectory(""), "path");
        }

        [TestMethod]
        public void WhenCreateSubdirectoryIsCalled_ThenSubdirectoryIsCreated()
        {
            _directoryInfo.CreateSubdirectory("xxx");
            var path = Path.Combine(_directoryInfo.FullName, "xxx");

            Assert.IsTrue(Directory.Exists(path));
            Directory.Delete(path);
        }

        private static System.IO.DirectoryInfo CreateTempDirectoryInfo()
        {
            var tempDirectoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var systemDirectoryInfo = new System.IO.DirectoryInfo(tempDirectoryPath);
            return systemDirectoryInfo;
        }
    }
}