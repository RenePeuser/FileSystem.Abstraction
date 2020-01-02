using FileSystem.Abstraction.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem.DirectoryService
{
    [TestClass]
    public abstract class TestBase
    {
        protected IDirectoryService TestObject { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            TestObject = new Services.DirectoryService();
        }
    }
}