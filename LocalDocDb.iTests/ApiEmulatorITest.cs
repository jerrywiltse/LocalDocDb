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
    public class ApiEmulatorITest
    {
        private readonly ITestOutputHelper _testOutput;
        private readonly bool _debugToConsole = true;
        public ApiEmulatorITest(ITestOutputHelper helper)
        {
            _testOutput = helper;
        }       
        [Fact]  
        public void Create_Account_Directory_Using_Default_Root()
        {
            //given
            string testAccountName = "testAccount01";
            string expectedPath= @"DocumentDBRoot/accounts/testAccount01";
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
            DirectoryInfo pathFull = new DirectoryInfo(Path.Combine(expectedPathFull,"dbs"));
            Directory.Delete(pathFull.FullName);
        }
        [Fact]
        public void Create_Account_Directory_Using_Custom_Root()
        {
            //given
            string testAccountName = "testAccount02";
            string testPathRoot = "DocDbTestRoot02";
            string expectedPath = @"DocDbTestRoot02/accounts/testAccount02";
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
        [Fact]
        public void Recycle_Account_Directory()
        {
            //given
            string testAccountName = "testAccount03";
            string expectedPath = @"DocumentDBRoot/accounts/testAccount03";
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
        [Fact]
        public void Submit_List_Databases()
        {
            //given
            string testAccountName = "testAccount03";
            string expectedPath = @"DocumentDBRoot/accounts/testAccount03";
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);
            if (_debugToConsole) _testOutput.WriteLine(sut.AccountFullPath.FullName);
            sut.CreateAccountDir();

            //when
            Response response = sut.Submit(HttpOperation.GET, "dbs");
            if (_debugToConsole) _testOutput.WriteLine(response.ToString());

            //then
            Assert.Equal(200, response.Code);

            //cleanup
            DirectoryInfo pathFull = new DirectoryInfo(expectedPathFull);
            Directory.Delete(Path.Combine(pathFull.FullName, "dbs"));
            Directory.Delete(pathFull.FullName);
        }
        [Fact]
        public void Submit_Create_Databases()
        {
            //given
            string testDbName = "CreateDatabaseTest";
            string testAccountName = "CreateDatabaseTestAccount";
            string expectedPath = @"DocumentDBRoot/accounts/CreateDatabaseTestAccount/CreateDatabaseTest";
            var sut = new ApiEmulator(testAccountName);
            var systemRoot = Path.GetPathRoot(Environment.SystemDirectory);
            var expectedPathFull = Path.Combine(systemRoot, expectedPath);
            if (_debugToConsole) _testOutput.WriteLine(sut.AccountFullPath.FullName);
            sut.CreateAccountDir();

            //and
            string jsonBody = @"{
                
            }";

            //when
            Response response = sut.Submit(HttpOperation.POST, "dbs", testDbName);
            if (_debugToConsole) _testOutput.WriteLine(response.ToString());

            //then
            Assert.Equal(200, response.Code);

            //cleanup
            DirectoryInfo pathFull = new DirectoryInfo(expectedPathFull);
            Directory.Delete(Path.Combine(pathFull.FullName, "dbs"));
            Directory.Delete(pathFull.FullName);
        }
    }
}
