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
                "{ \"username\":\"4\",\"idNumber\":\"4\",\"phoneNumber\":\"4\",\"password\":\"4\"}," +
                "{ \"username\":\"Test123\",\"idNumber\":\"S1234567S\",\"phoneNumber\":\"99999990\",\"password\":\"Test123!\"}]";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFile);
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ABC")]
        [InlineData("ABC    CD")]
        [InlineData("Test123")]
        public void checkUserNameTestTrue(string username)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkUsername(username);
            Assert.True(res);
        }

        [Theory]
        [InlineData("Test12")]
        [InlineData("Test3455")]
        public void checkUserNameTestFalse(string username)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkUsername(username);
            Assert.False(res);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ABC")]
        [InlineData("1")]
        [InlineData("S1234567S")]
        public void checkIDTestTrue(string ID)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkID(ID);
            Assert.True(res);
        }

        [Theory]
        [InlineData("S1234566E")]
        [InlineData("T9123993A")]
        public void checkIDTestFalse(string ID)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkID(ID);
            Assert.False(res);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("ABC")]
        [InlineData("1")]
        [InlineData("912354A")]
        [InlineData("99999990")]
        public void checkPhoneNumberTestTrue(string phoneNumber)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPhoneNumber(phoneNumber);
            Assert.True(res);
        }

        [Theory]
        [InlineData("99999991")]
        [InlineData("99999992")]
        public void checkPhoneNumberTestFalse(string phoneNumber)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPhoneNumber(phoneNumber);
            Assert.False(res);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("AbC")]
        [InlineData("1")]
        [InlineData("912354A")]
        [InlineData("AvC@@##@")]
        public void checkPasswordTestFalse(string password)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPassword(password);
            Assert.True(res);
        }

        [Theory]
        [InlineData("ABC123!")]
        [InlineData("12314AAA!")]
        public void checkPasswordTestTrue(string password)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPassword(password);
            Assert.True(res);
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
        public void loginTestFalse(string username, string password)
        {
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.login(username, password);
            Assert.False(res);
        }


        [Theory]
        [InlineData("5", "5", "5", "5")]
        public void signUpTest(string username, string idNumber, string phoneNumber, string password)
        {
            string gamblerListStr = "[{\"username\":\"1\",\"idNumber\":\"1\",\"phoneNumber\":\"1\",\"password\":\"1\"}," +
                "{\"username\":\"2\",\"idNumber\":\"2\",\"phoneNumber\":\"2\",\"password\":\"2\"}," +
                "{\"username\":\"3\",\"idNumber\":\"3\",\"phoneNumber\":\"3\",\"password\":\"3\"}," +
                "{\"username\":\"4\",\"idNumber\":\"4\",\"phoneNumber\":\"4\",\"password\":\"4\"}," +
                "{\"username\":\"Test123\",\"idNumber\":\"S1234567S\",\"phoneNumber\":\"99999990\",\"password\":\"Test123!\"}," +
                "{\"username\":\"5\",\"idNumber\":\"5\",\"phoneNumber\":\"5\",\"password\":\"5\"}]";
            _mockFileHandling.SetupAllProperties();
            _mockFileHandling.Setup(t => t.writeAllText("Gambler.json", gamblerListStr)).Verifiable();
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.signup(username, idNumber, phoneNumber, password);
            //_mockFileHandling.Verify(t => t.writeAllText("Gambler.json", gamblerListStr), Times.AtLeastOnce);
            Assert.Equal(gamblerListStr, res);
            _mockFileHandling.Verify();
        }

        [Theory]
        [InlineData("5", "5", "5", "5")]
        public void signUpTestNull(string username, string idNumber, string phoneNumber, string password)
        {
            string gamblerJsonFileNull = "";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFileNull);
            string gamblerListStr = "[{\"username\":\"5\",\"idNumber\":\"5\",\"phoneNumber\":\"5\",\"password\":\"5\"}]";
            _mockFileHandling.SetupAllProperties();
            _mockFileHandling.Setup(t => t.writeAllText("Gambler.json", gamblerListStr)).Verifiable();
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.signup(username, idNumber, phoneNumber, password);
            //_mockFileHandling.Verify(t => t.writeAllText("Gambler.json", gamblerListStr), Times.AtLeastOnce);
            Assert.Equal(gamblerListStr, res);
            _mockFileHandling.Verify();
        }



        [Theory]
        [InlineData("Test123")]
        [InlineData("Happy123")]
        public void checkUserNameTestNullFalse(string username)
        {
            string gamblerJsonFileNull = "";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFileNull);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkUsername(username);
            Assert.False(res);
        }

        [Theory]
        [InlineData("S1234566E")]
        [InlineData("T9123993A")]
        public void checkIDTestNullFalse(string ID)
        {
            string gamblerJsonFileNull = "";
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Returns(gamblerJsonFileNull);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkID(ID);
            Assert.False(res);
        }

        [Theory]
        [InlineData("99999991")]
        [InlineData("99999992")]
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
            IOException IO = new IOException();
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Throws(IO);
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.checkPhoneNumber(phoneNumber);
            Assert.True(res);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("3", "4")]
        public void loginTestException(string username, string password)
        {
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Throws(new IOException());
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.login(username, password);
            Assert.False(res);
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

        [Theory]
        [InlineData("5", "5", "5", "5")]
        public void signUpExceptionTest(string username, string idNumber, string phoneNumber, string password)
        {
            _mockFileHandling.SetupAllProperties();
            _mockFileHandling.Setup(t => t.readAllText("Gambler.json")).Throws(new IOException());
            UserAuthentication UA = new UserAuthentication(_mockFileHandling.Object);
            var res = UA.signup(username, idNumber, phoneNumber, password);
            Assert.Equal("", res);
            _mockFileHandling.Verify();
        }
    }
}
