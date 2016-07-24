using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xunit.Abstractions;
using LocalDocDb;

namespace LocalDocDb.iTests
{
    public class ApiEmulatorTest
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly bool _debugToConsole = true;
        public ApiEmulatorTest(ITestOutputHelper helper)
        {
            _testOutput = helper;
        }
        [Fact]
        public void GetDefaultPath()
        {
            //given
            string testAccountName = "testAccount00";
            string expectedPath = @"DocumentDBRoot/accounts/testAccount00";
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);

            //when
            DirectoryInfo returnPath = sut.GetDefaultPath(testAccountName);

            //then
            if (_debugToConsole) _testOutput.WriteLine(returnPath.FullName);
            Assert.Contains(returnPath.FullName.Replace(@"\", "/"), expectedPathFull.Replace(@"\","/"));
        }
        [Fact]
        public void GetCustomPath()
        {
            //given
            string testAccountName = "testAccount00";
            string testPathRoot = "DocDbTestRoot";
            string expectedPath = @"DocDbTestRoot/accounts/testAccount00";
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);
            var testPathRootFull = Path.Combine(systemRoot, testPathRoot);

            //when
            DirectoryInfo returnPath = sut.GetCustomPath(testAccountName, testPathRootFull);

            //then
            if (_debugToConsole) _testOutput.WriteLine(returnPath.FullName);
            Assert.Contains(returnPath.FullName.Replace(@"\", "/"), expectedPathFull.Replace(@"\", "/"));
        }

    }
}
