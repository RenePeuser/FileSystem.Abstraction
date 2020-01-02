using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem
{
    [TestClass]
    public class FileServiceTest
    {
        private const string Filename = "SomeFile.txt";

        private const string Path = @"C:\SomeDir\SomeFile.txt";

        private const string Content = "some content";

        [TestMethod]
        public void WhenGetFileInfoIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            var testObject = new FileService();

            Assert.ThrowsException<ArgumentException>(() => testObject.GetFileInfo(""));
        }

        [TestMethod]
        public void WhenGetFileInfoIsCalled_ThenInfoIsReturned()
        {
            var testObject = new FileService();
            var result = testObject.GetFileInfo(Path);

            Assert.IsNotNull(result);
            Assert.AreEqual(Path, result.FullName);
        }
    }
}
