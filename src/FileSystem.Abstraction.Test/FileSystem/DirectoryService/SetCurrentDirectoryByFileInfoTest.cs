using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem.DirectoryService
{
    [TestClass]
    public class SetCurrentDirectoryByFileInfoTest : TestBase
    {
        [TestMethod]
        public void When_SetCurrentDirectoryInfo_Is_Called_With_Null_FileInfo_Then_ArgumentExceptions_Have_To_Be_Thrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.SetCurrentDirectoryInfo((FileInfo) null));
        }

        [TestMethod]
        public void When_SetCurrentDirectoryInfo_Is_Called_With_NullDirectoryInfo_Then_ArgumentExceptions_Have_To_Be_Thrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.SetCurrentDirectoryInfo((DirectoryInfo) null));
        }

        [TestMethod]
        public void When_SetCurrentDirectoryInfo_Is_Called_With_FileInfo_Then_CurrentDirectory_Has_To_Be_As_Expected()
        {
            var virtualDirectory = new FileInfo(new System.IO.FileInfo(typeof(SetCurrentDirectoryByFileInfoTest).Assembly.FullName));

            Assert.AreEqual(TestObject.GetCurrentDirectory().FullName, TestObject.SetCurrentDirectoryInfo(virtualDirectory).FullName);
        }

        [TestMethod]
        public void When_SetCurrentDirectoryInfo_Is_Called_With_DirectoryInfo_Then_CurrentDirectory_Has_To_Be_As_Expected()
        {
            var virtualDirectory = new DirectoryInfo(new System.IO.FileInfo(typeof(SetCurrentDirectoryByFileInfoTest).Assembly.FullName).Directory);

            Assert.AreEqual(TestObject.GetCurrentDirectory().FullName, TestObject.SetCurrentDirectoryInfo(virtualDirectory).FullName);
        }
    }
}
