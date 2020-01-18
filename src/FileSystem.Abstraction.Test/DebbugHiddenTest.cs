using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSystem.Abstraction.Test
{
    [TestClass]
    public class DebbugHiddenTest
    {
        private static IEnumerable<CSharpFileInfo> _csharpFileInfos;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            var currentDirectory = new System.IO.DirectoryInfo(Environment.CurrentDirectory);

            var argumentCheckDirectory = FindFolderWithSources(currentDirectory, "FileSystem.Abstraction");
            var allCSharpFiles = argumentCheckDirectory.EnumerateFiles("*.cs", SearchOption.AllDirectories);
            _csharpFileInfos = allCSharpFiles.Select(csharpFile =>
                {
                    var syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(csharpFile.FullName));
                    return new CSharpFileInfo(csharpFile, syntaxTree);
                })
                .ToList();
        }

        [TestMethod]
        public void All_Methods_From_All_Classes_In_ArgumentCheck_Folder_Must_Be_Decorated_With_DebuggerHidden_Attribute()
        {
            var missingHiddenAttribute = from cSharpFileInfo in _csharpFileInfos
                                         where cSharpFileInfo.FileInfo.FullName.Contains("ArgumentCheck")
                                         from @class in cSharpFileInfo.SyntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
                                         from method in @class.DescendantNodes().OfType<MethodDeclarationSyntax>()
                                         let attributeListSyntaxes = method.AttributeLists
                                         where !attributeListSyntaxes.Any() || !attributeListSyntaxes.First().Attributes.Any() || !attributeListSyntaxes.First().Attributes.Any(a => a.Name.ToString().Contains("DebuggerHidden"))
                                         select new AnalyzeResult(cSharpFileInfo.FileInfo, method.Identifier.ToString());

            Assert.IsFalse(missingHiddenAttribute.Any(), ToMessage(missingHiddenAttribute));
        }

        private static System.IO.DirectoryInfo FindFolderWithSources(System.IO.DirectoryInfo startDirectoryInfo, string name)
        {
            if (startDirectoryInfo == null)
            {
                return null;
            }

            if (startDirectoryInfo.Name == name)
            {
                return startDirectoryInfo;
            }

            var directory = startDirectoryInfo.EnumerateDirectories(name).FirstOrDefault();
            if (directory != null)
            {
                return directory;
            }

            return FindFolderWithSources(startDirectoryInfo.Parent, name);
        }

        private string ToMessage(IEnumerable<AnalyzeResult> analyzeResults)
        {
            var groups = analyzeResults.GroupBy(result => result.CSharpFileInfo.FullName);
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Missing {nameof(DebuggerHiddenAttribute)} detected:");
            stringBuilder.AppendLine();
            foreach (var group in groups)
            {
                stringBuilder.AppendLine(group.Key);
                foreach (var analyzeResult in group)
                {
                    stringBuilder.AppendLine($"- {analyzeResult.MethodName}");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        [DebuggerDisplay("{CSharpFileInfo.Name}")]
        private class AnalyzeResult
        {
            public AnalyzeResult(System.IO.FileInfo cSharpFileInfo, string methodName)
            {
                CSharpFileInfo = cSharpFileInfo;
                MethodName = methodName;
            }

            public System.IO.FileInfo CSharpFileInfo { get; }

            public string MethodName { get; }
        }

        [DebuggerDisplay("{FileInfo.Name}")]
        private class CSharpFileInfo
        {
            public CSharpFileInfo(System.IO.FileInfo fileInfo, SyntaxTree syntaxTree)
            {
                FileInfo = fileInfo;
                SyntaxTree = syntaxTree;
            }

            public System.IO.FileInfo FileInfo { get; }

            public SyntaxTree SyntaxTree { get; }
        }
    }
}
