using Moq;
using OnlineCasinoProjectConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineCasinoTesting
{
    public class OwnerClassTest
    {
        private Mock<IFileHandling> _mockFileHandling;

        public OwnerClassTest()
        {
            _mockFileHandling = new Mock<IFileHandling>(MockBehavior.Strict);

            string ownerJsonFile = "[{\"username\":\"1\",\"password\":\"1\"}]";
            _mockFileHandling.Setup(t => t.readAllText("Owner.json")).Returns(ownerJsonFile);
        }

        [Theory]
        [InlineData("1","1")]
        public void ownerLoginTestTrue(string username, string password)
        {
            Owner ownerTest = new Owner(_mockFileHandling.Object);
            var res = ownerTest.ownerLogin(username, password);
            Assert.True(res);
        }

        [Theory]
        [InlineData("3", "2")]
        public void ownerLoginTestFalse1(string username, string password)
        {
            Owner ownerTest = new Owner(_mockFileHandling.Object);
            var res = ownerTest.ownerLogin(username, password);
            Assert.False(res);
        }

        [Theory]
        [InlineData("1", "2")]
        public void ownerLoginTestFalse2(string username, string password)
        {
            Owner ownerTest = new Owner(_mockFileHandling.Object);
            var res = ownerTest.ownerLogin(username, password);
            Assert.False(res);
        }

        [Theory]
        [InlineData("2", "2")]
        public void ownerLoginFalseNull(string username, string password)
        {
            string ownerJsonFile = "";
            _mockFileHandling.Setup(t => t.readAllText("Owner.json")).Returns(ownerJsonFile);
            Owner ownerTest = new Owner(_mockFileHandling.Object);
            var res = ownerTest.ownerLogin(username, password);
            Assert.False(res);
        }

        [Theory]
        [InlineData(1)]
        public void setPrizeModuleTrue(int input)
        {
            Owner ownerTest = new Owner(_mockFileHandling.Object);
            var res = ownerTest.setPrizeModule(input);
            Assert.True(res);
        }

        [Theory]
        [InlineData(2)]
        public void setPrizeModuleFalse(int input)
        {
            Owner ownerTest = new Owner(_mockFileHandling.Object);
            var res = ownerTest.setPrizeModule(input);
            Assert.False(res);
        }

        [Theory]
        [InlineData(3)]
        public void setPrizeModuleInvalid(int input)
        {
            Owner ownerTest = new Owner(_mockFileHandling.Object);
            var res = ownerTest.setPrizeModule(input);
            Assert.IsType<bool>(res);
        }

    }
}
