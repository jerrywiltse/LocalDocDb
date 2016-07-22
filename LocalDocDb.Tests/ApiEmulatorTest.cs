using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xunit.Abstractions;

namespace LocalDocDbClient.Tests
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
        [InlineData("testAccount00", @"DocumentDBRoot/accounts/testAccount00")]
        public void GetDefaultPath(string testAccountName, string expectedPath)
        {
            //given
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);

            //when
            DirectoryInfo returnPath = sut.GetDefaultPath(testAccountName);

            //then
            if (_debugToConsole) _testOutput.WriteLine(returnPath.FullName);
            Assert.Equal(returnPath.FullName.Replace(@"\", "/"), expectedPathFull.Replace(@"\","/"));
        }
        [Theory]
        [InlineData("testAccount00", "DocDbTestRoot", @"DocDbTestRoot/accounts/testAccount00")]
        public void GetCustomPath(string testAccountName, string testPathRoot, string expectedPath)
        {
            //given
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);
            var testPathRootFull = Path.Combine(systemRoot, testPathRoot);

            //when
            DirectoryInfo returnPath = sut.GetCustomPath(testAccountName, testPathRootFull);

            //then
            if (_debugToConsole) _testOutput.WriteLine(returnPath.FullName);
            Assert.Equal(returnPath.FullName.Replace(@"\", "/"), expectedPathFull.Replace(@"\", "/"));
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
        [InlineData("testAccount02", "DocDbTestRoot02", @"DocDbTestRoot02/accounts/testAccount02")]
        public void CreateAccountDirectoryUsingCustomRoot(string testAccountName, string testPathRoot, string expectedPath)
        {
            //given
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);
            var testPathRootFull = Path.Combine(systemRoot, testPathRoot);
            if (_debugToConsole) _testOutput.WriteLine(testPathRootFull);
            var sut = new ApiEmulator(testAccountName, testPathRootFull);

            //when
            sut.CreateAccountDir();

            //then
            if (_debugToConsole) _testOutput.WriteLine(expectedPathFull);
            Assert.True(Directory.Exists(expectedPathFull));

            //cleanup
            //Note: Yes, we could use Parent.Parent recursive. Purposesly opted against for safety reasons.
            DirectoryInfo pathFull = new DirectoryInfo(expectedPathFull);
            Directory.Delete(Path.Combine(pathFull.FullName,"dbs"));
            Directory.Delete(pathFull.FullName);
            Directory.Delete(pathFull.Parent.FullName);
            Directory.Delete(pathFull.Parent.Parent.FullName);
        }
        [Theory]
        [InlineData("testAccount03", @"DocumentDBRoot/accounts/testAccount03")]
        public void RecycleAccountDirectory(string testAccountName, string expectedPath)
        {
            //given
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);
            sut.CreateAccountDir();
            Assert.True(Directory.Exists(expectedPathFull));

            //when
            if (_debugToConsole) _testOutput.WriteLine(expectedPathFull);
            sut.ReycleAccountDir();

            //then
            Assert.False(Directory.Exists(expectedPathFull));
        }
        [Theory]
        [InlineData("testAccount03",@"DocumentDBRoot/accounts/testAccount03")]
        public void ListDatabases(string testAccountName, string expectedPath)
        {
            //given
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);
            if (_debugToConsole) _testOutput.WriteLine(sut.AccountFullPath.FullName);
            sut.CreateAccountDir();

            //when
            Response response = sut.Submit(HttpOperation.GET, "dbs");
            if (_debugToConsole) _testOutput.WriteLine(response.ToString());

            //then
            Assert.Equal(response.Code, 200);

            //cleanup
            DirectoryInfo pathFull = new DirectoryInfo(expectedPathFull);
            Directory.Delete(Path.Combine(pathFull.FullName, "dbs"));
            Directory.Delete(pathFull.FullName);
        }
    }
}
