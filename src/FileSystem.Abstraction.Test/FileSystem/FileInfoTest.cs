using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test.FileSystem
{
    [TestClass]
    public class FileInfoTest
    {
        private const string TextData = "LineA\r\nLineB\r\n";
        private const string TextDataAppend = "LineC\r\n";
        private const string TextDataFull = TextData + TextDataAppend;
        private static readonly string[] STextLines = { "LineA", "LineB" };
        private static readonly string[] STextLinesAppend = { "LineC" };
        private static System.IO.FileInfo _systemReadOnlyFileInfo;
        private FileInfo _testObject;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _systemReadOnlyFileInfo = CreateTempSystemFileInfo();
            File.WriteAllLines(_systemReadOnlyFileInfo.FullName, STextLines);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if (_systemReadOnlyFileInfo != null && _systemReadOnlyFileInfo.Exists)
            {
                _systemReadOnlyFileInfo.IsReadOnly = false;
                _systemReadOnlyFileInfo.Delete();
            }
        }

        [TestInitialize]
        public void Initialize()
        {
            _testObject = new FileInfo(_systemReadOnlyFileInfo);
        }

        [TestMethod]
        public void WhenFileInfoIsCreatedWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FileInfo(null), "info");
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenDirectoryIsSet()
        {
            Assert.AreEqual(_systemReadOnlyFileInfo.Directory.FullName, _testObject.Directory.FullName);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenDirectoryNameIsSet()
        {
            Assert.AreEqual(_systemReadOnlyFileInfo.DirectoryName, _testObject.DirectoryName);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenIsReadOnlyIsSet()
        {
            Assert.AreEqual(_systemReadOnlyFileInfo.IsReadOnly, _testObject.IsReadOnly);
        }

        [TestMethod]
        public void WhenIsReadonlyChanged_ThenIsReadOnlyIsUpdated()
        {
            _testObject.IsReadOnly = !_testObject.IsReadOnly;
            Assert.AreEqual(_systemReadOnlyFileInfo.IsReadOnly, _testObject.IsReadOnly);
        }

        [TestMethod]
        public void WhenFileInfoIsCreated_ThenLengthIsSet()
        {
            Assert.AreEqual(_systemReadOnlyFileInfo.Length, _testObject.Length);
        }

        [TestMethod]
        public void WhenReadAllBytesIsCalled_ThenCorrectDataIsReturned()
        {
            var systemReadBytes = File.ReadAllBytes(_systemReadOnlyFileInfo.FullName);
            var testReadBytes = _testObject.ReadAllBytes();
            Assert.IsTrue(systemReadBytes.SequenceEqual(testReadBytes));
        }

        [TestMethod]
        public void WhenReadAllLinesIsCalled_ThenCorrectDataIsReturned()
        {
            var systemReadLines = File.ReadAllLines(_systemReadOnlyFileInfo.FullName);
            var testReadLines = _testObject.ReadAllLines();
            Assert.IsTrue(systemReadLines.SequenceEqual(testReadLines));
        }

        [TestMethod]
        public void WhenReadAllLinesWithEncodingIsCalled_ThenCorrectDataIsReturned()
        {
            var systemReadLines = File.ReadAllLines(_systemReadOnlyFileInfo.FullName, Encoding.UTF8);
            var testReadLines = _testObject.ReadAllLines(Encoding.UTF8);
            Assert.IsTrue(systemReadLines.SequenceEqual(testReadLines));
        }

        [TestMethod]
        public void WhenReadAllTextIsCalled_ThenCorrectDataIsReturned()
        {
            var systemReadText = File.ReadAllText(_systemReadOnlyFileInfo.FullName);
            var testReadText = _testObject.ReadAllText();
            Assert.IsTrue(systemReadText.SequenceEqual(testReadText));
        }

        [TestMethod]
        public void WhenReadAllTextWithEncodingIsCalled_ThenCorrectDataIsReturned()
        {
            var systemReadText = File.ReadAllText(_systemReadOnlyFileInfo.FullName, Encoding.UTF8);
            var testReadText = _testObject.ReadAllText(Encoding.UTF8);
            Assert.IsTrue(systemReadText.SequenceEqual(testReadText));
        }

        [TestMethod]
        public void WhenOpenReadIsCalled_ThenReadableStreamIsReturned()
        {
            using (var stream = _testObject.OpenRead())
            {
                var text = ReadStream(stream);
                Assert.AreEqual(TextData, text);
            }
        }

        [TestMethod]
        public void WhenOpenIsCalled_ThenReadableStreamIsReturned()
        {
            using (var stream = _testObject.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var text = ReadStream(stream);
                Assert.AreEqual(TextData, text);
            }
        }

        [TestMethod]
        public void WhenOpenTextIsCalled_ThenReadableStreamIsReturned()
        {
            using (var stream = _testObject.OpenText())
            {
                var text = stream.ReadToEnd();
                Assert.AreEqual(TextData, text);
            }
        }

        [TestMethod]
        public void WhennWriteAllTextIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.WriteAllText(null), "contents");
        }

        [TestMethod]
        public void WhenWriteAllTextIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(testObject => testObject.WriteAllText(TextData));
        }

        [TestMethod]
        public void WhennWriteAllBytesIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.WriteAllBytes(null), "contents");
        }

        [TestMethod]
        public void WhenWriteAllBytesIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(testObject => testObject.WriteAllBytes(Encoding.UTF8.GetBytes(TextData)));
        }

        [TestMethod]
        public void WhennWriteAllTextWithEncodingIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.WriteAllText(null, Encoding.UTF8), "contents");
        }

        [TestMethod]
        public void WhenWriteAllTextWithEncodingIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(testObject => testObject.WriteAllText(TextData, Encoding.UTF8));
        }

        [TestMethod]
        public void WhennWriteAllLinesIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.WriteAllLines(null), "contents");
        }

        [TestMethod]
        public void WhenWriteAllLinesIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(testObject => testObject.WriteAllLines(STextLines));
        }

        [TestMethod]
        public void WhennWriteAllLinesWithEncodingIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.WriteAllLines(null, Encoding.UTF8), "contents");
        }

        [TestMethod]
        public void WhenWriteAllLinesWithEncodingIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(testObject => testObject.WriteAllLines(STextLines, Encoding.UTF8));
        }

        [TestMethod]
        public void WhenAppendAllTextIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.AppendAllText(null), "contents");
        }

        [TestMethod]
        public void WhenAppendAllTextIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(
                testObject =>
                    {
                        testObject.WriteAllText(TextData);
                        testObject.AppendAllText(TextDataAppend);
                    },
                TextDataFull);
        }

        [TestMethod]
        public void WhenAppendAllTextWithEncodingIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.AppendAllText(null, Encoding.UTF8), "contents");
        }

        [TestMethod]
        public void WhenAppendAllTextWithEncodingIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(
                testObject =>
                    {
                        testObject.WriteAllText(TextData, Encoding.UTF8);
                        testObject.AppendAllText(TextDataAppend, Encoding.UTF8);
                    },
                TextDataFull);
        }

        [TestMethod]
        public void WhenAppendAllLinesIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.AppendAllLines(null), "contents");
        }

        [TestMethod]
        public void WhenAppendAllLinesIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(
                testObject =>
                    {
                        testObject.WriteAllLines(STextLines);
                        testObject.AppendAllLines(STextLinesAppend);
                    },
                TextDataFull);
        }

        [TestMethod]
        public void WhenAppendAllLinesWithEncodingIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _testObject.AppendAllLines(null, Encoding.UTF8), "contents");
        }

        [TestMethod]
        public void WhenAppendAllLinesWithEncodingIsCalled_ThenFileIsUpdated()
        {
            AssertFileContent(
                testObject =>
                    {
                        testObject.WriteAllLines(STextLines, Encoding.UTF8);
                        testObject.AppendAllLines(STextLinesAppend, Encoding.UTF8);
                    },
                TextDataFull);
        }

        [TestMethod]
        public void WhenOpenWriteIsCalled_ThenWritableStreamIsReturned()
        {
            AssertFileContent(
                testObject =>
                    {
                        using (var stream = testObject.OpenWrite())
                        {
                            var bytes = Encoding.UTF8.GetBytes(TextData);
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    });
        }

        [TestMethod]
        public void WhenCreateIsCalled_ThenWritableStreamIsReturned()
        {
            AssertFileContent(
                testObject =>
                    {
                        using (var stream = testObject.Create())
                        {
                            var bytes = Encoding.UTF8.GetBytes(TextData);
                            stream.Write(bytes, 0, bytes.Length);
                        }
                    });
        }

        [TestMethod]
        public void WhenCreateTextIsCalled_ThenWritableStreamIsReturned()
        {
            AssertFileContent(
                testObject =>
                    {
                        using (var stream = testObject.CreateText())
                        {
                            stream.Write(TextData);
                        }
                    });
        }

        [TestMethod]
        public void WhenAppendTextIsCalled_ThenWritableStreamIsReturned()
        {
            AssertFileContent(
                testObject =>
                    {
                        testObject.WriteAllText(TextData);

                        using (var stream = testObject.AppendText())
                        {
                            stream.Write(TextDataAppend);
                        }
                    },
                TextDataFull);
        }

        [TestMethod]
        public void WhenCopyToIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _testObject.CopyTo("", false), "destinationFileName");
        }

        [TestMethod]
        public void WhenCopyToIsCalled_ThenFileIsCopied()
        {
            var targetFile = CreateTempSystemFileInfo();
            Assert.IsFalse(targetFile.Exists);

            _testObject.CopyTo(targetFile.FullName, false);
            targetFile.Refresh();
            Assert.IsTrue(targetFile.Exists);

            targetFile.Attributes = FileAttributes.Normal;
            targetFile.Delete();
        }

        [TestMethod]
        public void WhenMoveToIsCalledWithInvalidArguments_ThenExceptionIsThrown()
        {
            Assert.ThrowsException<ArgumentException>(() => _testObject.MoveTo(""), "destinationFileName");
        }

        [TestMethod]
        public void WhenMoveToIsCalled_ThenFileIsMoved()
        {
            var file = CreateTempSystemFileInfo();
            var testObject = new FileInfo(file);
            testObject.WriteAllText(TextData);
            Assert.IsTrue(testObject.Exists);

            var targetFile = CreateTempSystemFileInfo();
            Assert.IsFalse(targetFile.Exists);

            testObject.MoveTo(targetFile.FullName);
            targetFile.Refresh();
            testObject.Refresh();

            Assert.IsTrue(targetFile.Exists);
            Assert.AreEqual(targetFile.FullName, testObject.FullName);

            targetFile.Delete();
        }

        private static string ReadStream(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            var text = Encoding.UTF8.GetString(bytes);
            return text;
        }

        private static void AssertFileContent(Action<FileInfo> action, string expectedContent = TextData)
        {
            var tempFile = CreateTempSystemFileInfo();
            var testObject = new FileInfo(tempFile);

            try
            {
                action(testObject);

                var result = File.ReadAllText(testObject.FullName);
                Assert.AreEqual(expectedContent, result);
            }
            finally
            {
                tempFile.Delete();
            }
        }

        private static System.IO.FileInfo CreateTempSystemFileInfo()
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            var systemFileInfo = new System.IO.FileInfo(tempFilePath);
            return systemFileInfo;
        }
    }
}