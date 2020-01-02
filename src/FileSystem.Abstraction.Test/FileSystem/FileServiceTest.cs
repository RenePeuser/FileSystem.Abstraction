using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem
{
    [TestClass]
    public class FileServiceTest
    {
        [TestMethod]
        public void WhenGetFileInfoIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            var testObject = new FileService();

            Assert.ThrowsException<ArgumentException>(() => testObject.GetFileInfo(""));
        }

        [TestMethod]
        public void WhenGetFileInfoIsCalled_ThenInfoIsReturned()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "SomeDir", "SomeFile.txt");

            var testObject = new FileService();
            var result = testObject.GetFileInfo(path);

            Assert.IsNotNull(result);
            Assert.AreEqual(path, result.FullName);
        }
    }
}
