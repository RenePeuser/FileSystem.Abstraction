using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem.DirectoryService
{
    [TestClass]
    public class DirectoryServiceTest : TestBase
    {
        [TestMethod]
        public void WhenGetDirectoryInfoIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => TestObject.GetDirectoryInfo(""), "path");
        }

        [TestMethod]
        public void WhenGetDirectoryInfoIsCalled_ThenInfoIsReturned()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "SomeDir", "SomeSubDir");

            var result = TestObject.GetDirectoryInfo(path);

            Assert.IsNotNull(result);
            Assert.AreEqual(path, result.FullName);
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
