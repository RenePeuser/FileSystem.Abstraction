using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem.DirectoryService
{
    [TestClass]
    public class CreateDirectoryTest : TestBase
    {
        [TestMethod]
        public void WhenCombineIsCalledWithNull_ThenExpectedExceptionHasToBeThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.CreateDirectory(null), "directoryName");
        }

        [TestMethod]
        public void WhenCombineIsCalledWithWhitespace_ThenExpectedExceptionHasToBeThrown()
        {
            var directoryName = "MyDirectory";
            var directoryInfo = TestObject.CreateDirectory(directoryName);

            Assert.AreEqual(directoryName, directoryInfo.Name);
        }
    }
}
