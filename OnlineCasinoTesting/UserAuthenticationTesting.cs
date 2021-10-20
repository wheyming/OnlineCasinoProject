using System;
using Xunit;
using Moq;
using OnlineCasinoProjectConsole;
using System.IO;

namespace OnlineCasinoTesting
{
    public class UserAuthenticationTesting
    {
        private Mock<IFileHandling> _mockFileHandling;

        public UserAuthenticationTesting()
        {
            _mockFileHandling = new Mock<IFileHandling>(MockBehavior.Strict);

            string gamblerJsonFile = "[{ \"username\":\"1\",\"idNumber\":\"1\",\"phoneNumber\":\"1\",\"password\":\"1\"}," +
                "{ \"username\":\"2\",\"idNumber\":\"2\",\"phoneNumber\":\"2\",\"password\":\"2\"}," +
                "{ \"username\":\"3\",\"idNumber\":\"3\",\"phoneNumber\":\"3\",\"password\":\"3\"}," +
                "{ \"username\":\"4\",\"idNumber\":\"4\",\"phoneNumber\":\"4\",\"password\":\"4\"}]";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFile);
        }


        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void checkUserNameTestTrue(string username)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkUsername(username);
            Assert.True(res);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ABC")]
        public void checkUserNameTestFalse(string username)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkUsername(username);
            Assert.False(res);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void checkIDTestTrue(string ID)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkID(ID);
            Assert.True(res);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ABC")]
        public void checkIDTestFalse(string ID)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkID(ID);
            Assert.False(res);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void checkPhoneNumberTestTrue(string phoneNumber)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPhoneNumber(phoneNumber);
            Assert.True(res);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ABC")]
        public void checkPhoneNumberTestFalse(string phoneNumber)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPhoneNumber(phoneNumber);
            Assert.False(res);
        }

        [Theory]
        [InlineData("1", "1")]
        [InlineData("3", "3")]
        public void loginTestTrue(string username, string password)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.login(username, password);
            Assert.True(res);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("3", "4")]
        public void loginTestFalse (string username, string password)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.login(username, password);
            Assert.False(res);
        }


        [Theory]
        [InlineData("5", "5", "5", "5")]
        public void signUpTest(string username, string idNumber, string phoneNumber, string password)
        {
            string gamblerListStr = "[{ \"username\":\"1\",\"idNumber\":\"1\",\"phoneNumber\":\"1\",\"password\":\"1\"}," +
                "{ \"username\":\"2\",\"idNumber\":\"2\",\"phoneNumber\":\"2\",\"password\":\"2\"}," +
                "{ \"username\":\"3\",\"idNumber\":\"3\",\"phoneNumber\":\"3\",\"password\":\"3\"}," +
                "{ \"username\":\"4\",\"idNumber\":\"4\",\"phoneNumber\":\"4\",\"password\":\"4\"}," +
                "{ \"username\":\"5\",\"idNumber\":\"5\",\"phoneNumber\":\"5\",\"password\":\"5\"}]";
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            _mockFileHandling.Setup(t => t.writeAllText("Gambler.json", gamblerListStr));
            var res = UA.signup(username, idNumber, phoneNumber, password);
            Assert.Equal(res, gamblerListStr);
        }



        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void checkUserNameTestNullFalse(string username)
        {
            string gamblerJsonFileNull = "";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFileNull);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkUsername(username);
            Assert.False(res);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void checkIDTestNullFalse(string ID)
        {
            string gamblerJsonFileNull = "";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFileNull);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkID(ID);
            Assert.False(res);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void checkPhoneNumberTestNullFalse(string phoneNumber)
        {
            string gamblerJsonFileNull = "";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFileNull);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPhoneNumber(phoneNumber);
            Assert.False(res);
        }

        [Theory]
        [InlineData("1")]
        public void checkUserNameTestException(string username)
        {
            string gamblerJsonFileNull = "";
            IOException IO = new IOException();
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Throws(IO);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkUsername(username);
            Assert.True(res);
        }

        [Theory]
        [InlineData("1")]
        public void checkIDTestException(string ID)
        {
            string gamblerJsonFileNull = "";
            IOException IO = new IOException();
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Throws(IO);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkID(ID);
            Assert.True(res);
        }

        [Theory]
        [InlineData("1")]
        public void checkphoneNumberTestException(string phoneNumber)
        {
            string gamblerJsonFileNull = "";
            IOException IO = new IOException();
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Throws(IO);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPhoneNumber(phoneNumber);
            Assert.True(res);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("3", "4")]
        public void loginTestNull(string username, string password)
        {
            string gamblerJsonFileNull = "";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFileNull);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.login(username, password);
            Assert.False(res);
        }
    }
}
