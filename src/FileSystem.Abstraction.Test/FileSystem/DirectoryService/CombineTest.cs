using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem.DirectoryService
{
    [TestClass]
    public class CombineTest : TestBase
    {
        [TestMethod]
        public void WhenCombineIsCalledWithNull_ThenExpectedExceptionHasToBeThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.Combine(null), "paths");
        }

        [TestMethod]
        public void WhenCombineIsCalledWithWhitespace_ThenExpectedExceptionHasToBeThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => TestObject.Combine(" "), "paths");
        }

        [TestMethod]
        public void WhenCombineIsCalledWithAnyItemIsNull_ThenExpectedExceptionHasToBeThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => TestObject.Combine("a", null), "paths");
        }

        [TestMethod]
        public void WhenCombineIsCalledWithAnyItemIsWhitespace_ThenExpectedExceptionHasToBeThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => TestObject.Combine("a", " "), "paths");
        }

        [TestMethod]
        public void WhenCombineIsCalledWithValidArguments_ExpectedResultHasToBeReturned()
        {
            Assert.AreEqual(@"a\b", TestObject.Combine("a", "b"));
        }
    }
}
