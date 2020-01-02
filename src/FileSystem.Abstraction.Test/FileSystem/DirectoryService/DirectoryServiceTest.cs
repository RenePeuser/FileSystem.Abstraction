using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem.DirectoryService
{
    [TestClass]
    public class DirectoryServiceTest : TestBase
    {
        private const string Path = @"C:\SomeDir\SomeSubDir";

        [TestMethod]
        public void WhenGetDirectoryInfoIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => TestObject.GetDirectoryInfo(""), "path");
        }

        [TestMethod]
        public void WhenGetDirectoryInfoIsCalled_ThenInfoIsReturned()
        {
            var result = TestObject.GetDirectoryInfo(Path);

            Assert.IsNotNull(result);
            Assert.AreEqual(Path, result.FullName);
        }

        [TestMethod]
        public void WhenGetTempDirectoryIsCalled_ThenInfoIsReturned()
        {
            var result = TestObject.GetTempDirectory();

            Assert.IsNotNull(result);
            Assert.AreEqual(System.IO.Path.GetTempPath(), result.FullName);
        }

        [TestMethod]
        public void WhenGetCurrentDirectoryIsCalled_ThenInfoIsReturned()
        {
            var result = TestObject.GetCurrentDirectory();

            Assert.IsNotNull(result);
            Assert.AreEqual(Directory.GetCurrentDirectory(), result.FullName);
        }
    }
}
