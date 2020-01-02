using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem
{
    [TestClass]
    public class FileServiceTest
    {
        private static readonly string Path = $"C:{System.IO.Path.DirectorySeparatorChar}SomeDir{System.IO.Path.DirectorySeparatorChar}SomeFile.txt";

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
