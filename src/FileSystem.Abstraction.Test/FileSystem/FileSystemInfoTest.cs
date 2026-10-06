using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem
{
    [TestClass]
    public class FileSystemInfoTest
    {
        private static System.IO.FileInfo _systemFileInfo;
        private FileSystemInfoTestClass _testObject;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _systemFileInfo = CreateTempFile();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if (_systemFileInfo != null && _systemFileInfo.Exists)
            {
                _systemFileInfo.Delete();
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            _testObject = new FileSystemInfoTestClass(_systemFileInfo);
        }

        [TestMethod]
        public void WhenFileInfoIsCreatedWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FileSystemInfoTestClass(null), "info");
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenAttributesAreSet()
        {
            Assert.AreEqual(_systemFileInfo.Attributes, _testObject.Attributes);
        }

        [TestMethod]
        public void WhenAttributesAreChanged_ThenAttributesAreUpdated()
        {
            Assert.AreNotEqual(FileAttributes.ReadOnly, _testObject.Attributes);
            _testObject.Attributes = FileAttributes.ReadOnly;
            Assert.AreEqual(_testObject.Attributes, _systemFileInfo.Attributes);
            _testObject.Attributes = FileAttributes.Normal;
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenCreationTimeIsSet()
        {
            Assert.AreEqual(_systemFileInfo.CreationTime, _testObject.CreationTime);
        }

        [TestMethod]
        public void WhenCreationTimeIsChanged_ThenCreationTimeIsUpdated()
        {
            _testObject.CreationTime = DateTime.Now;
            _testObject.Refresh();
            Assert.AreEqual(_testObject.CreationTime, _systemFileInfo.CreationTime);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenLastAccessTimeIsSet()
        {
            Assert.AreEqual(_systemFileInfo.LastAccessTime, _testObject.LastAccessTime);
        }

        [TestMethod]
        public void WhenLastAccessTimeIsChanged_ThenLastAccessTimeIsUpdated()
        {
            _testObject.LastAccessTime = DateTime.Now;
            _testObject.Refresh();
            Assert.AreEqual(_testObject.LastAccessTime, _systemFileInfo.LastAccessTime);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenLastWriteTimeIsSet()
        {
            Assert.AreEqual(_systemFileInfo.LastWriteTime, _testObject.LastWriteTime);
        }

        [TestMethod]
        public void WhenLastWriteTimeIsChanged_ThenLastWriteTimeIsUpdated()
        {
            _testObject.LastWriteTime = DateTime.Now;
            _testObject.Refresh();
            Assert.AreEqual(_testObject.LastWriteTime, _systemFileInfo.LastWriteTime);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenExistsIsSet()
        {
            Assert.AreEqual(_systemFileInfo.Exists, _testObject.Exists);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenFullNameIsSet()
        {
            Assert.AreEqual(_systemFileInfo.FullName, _testObject.FullName);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenExtensionIsSet()
        {
            Assert.AreEqual(_systemFileInfo.Extension, _testObject.Extension);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenNameIsSet()
        {
            Assert.AreEqual(_systemFileInfo.Name, _testObject.Name);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenInstanceIsSet()
        {
            Assert.AreEqual(_systemFileInfo, _testObject.FileInfoInstance);
        }

        [TestMethod]
        public void WhenDeleteIsCalled_ThenFileIsDeleted()
        {
            var systemFileInfo = CreateTempFile();
            var testObject = new FileSystemInfoTestClass(systemFileInfo);

            Assert.IsTrue(testObject.Exists);

            testObject.Delete();
            testObject.Refresh();

            Assert.IsFalse(testObject.Exists);
        }

        private static System.IO.FileInfo CreateTempFile()
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var systemFileInfo = new System.IO.FileInfo(tempFilePath);
            File.WriteAllText(systemFileInfo.FullName, "foo");
            return systemFileInfo;
        }

        private class FileSystemInfoTestClass : FileSystemInfo<System.IO.FileInfo>
        {
            public FileSystemInfoTestClass(System.IO.FileInfo info)
                : base(info)
            {
            }

            public System.IO.FileInfo FileInfoInstance => Instance;

            public override bool IsFile { get; } = true;

            public override bool IsDirectory { get; } = false;
        }
    }
}
