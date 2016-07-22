using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xunit.Abstractions;

namespace LocalDocDb.Tests
{
    public class ApiEmulatorTest
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly bool _debugToConsole = true;
        public ApiEmulatorTest(ITestOutputHelper helper)
        {
            _testOutput = helper;
        }

        [Theory]
        [InlineData("testAccount01", @"DocumentDBRoot/accounts/testAccount01")]
        public void CreateAccountDirectoryUsingDefaultRoot(string testAccountName, string expectedPath)
        {
            //given
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);

            //when
            sut.CreateAccountDir();

            //then
            if (_debugToConsole) _testOutput.WriteLine(expectedPathFull);
            Assert.True(Directory.Exists(expectedPathFull));

            //cleanup
            //Note: Yes, we could use Parent.Parent recursive. Purposesly opted against for safety reasons.
            DirectoryInfo pathFull = new DirectoryInfo(expectedPathFull);
            Directory.Delete(pathFull.FullName);
        }
        [Theory]
        [InlineData("testAccount02", "DocDbTestRoot", @"DocDbTestRoot/accounts/testAccount02")]
        public void CreateAccountDirectoryUsingCustomRoot(string testAccountName, string testPathRoot, string expectedPath)
        {
            //given
            var sut = new ApiEmulator(testAccountName, testPathRoot);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(testAccountName, systemRoot, expectedPath);

            //when
            if (_debugToConsole) _testOutput.WriteLine(expectedPathFull);
            sut.CreateAccountDir();

            //then
            if (_debugToConsole) _testOutput.WriteLine(expectedPathFull);
            Assert.True(Directory.Exists(expectedPathFull));

            //cleanup
            //Note: Yes, we could use Parent.Parent recursive. Purposesly opted against for safety reasons.
            DirectoryInfo pathFull = new DirectoryInfo(expectedPathFull);
            Directory.Delete(pathFull.FullName);
            Directory.Delete(pathFull.Parent.FullName);
        }
        [Theory]
        [InlineData("testAccount03", @"DocumentDBRoot/accounts/testAccount03")]
        public void RecycleAccountDirectory(string testAccountName, string expectedPath)
        {
            //given
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(testAccountName, systemRoot, expectedPath);
            sut.CreateAccountDir();
            Assert.True(Directory.Exists(expectedPathFull));

            //when
            if (_debugToConsole) _testOutput.WriteLine(expectedPathFull);
            sut.ReycleAccountDir();

            //then
            Assert.False(Directory.Exists(expectedPathFull));
            
        }
    }

}
