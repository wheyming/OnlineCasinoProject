using Casino.Common;
using Casino.WebAPI.Controllers;
using Casino.WebAPI.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace Casino.WebAPI.UnitTest
{
    public class AuthenticationControllerTest
    {
        private readonly Mock<ICasinoContext> _mockCasinoContext;

        public AuthenticationControllerTest()
        {
            _mockCasinoContext = new Mock<ICasinoContext>(MockBehavior.Strict);
        }

        [Theory]
        [InlineData("Sad123")]
        public void CheckUserNameTestNone(string username)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var userNameResult = authenticationController.CheckUserName(username);
            Assert.Equal(UserNameResultType.None, userNameResult);
        }

        [Theory]
        [InlineData("Happy  y")]
        public void CheckUserNameTestContainsSpace(string username)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var userNameResult = authenticationController.CheckUserName(username);
            Assert.Equal(UserNameResultType.UserNameContainsSpace, userNameResult);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckUserNameTestLengthIncorrect(string username)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var userNameResult = authenticationController.CheckUserName(username);
            Assert.Equal(UserNameResultType.UserNameLengthIncorrect, userNameResult);
        }

        [Theory]
        [InlineData("Happy123")]
        public void CheckUserNameTestDuplicate(string username)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var userNameResult = authenticationController.CheckUserName(username);
            Assert.Equal(UserNameResultType.DuplicateUser, userNameResult);
        }

        [Theory]
        [InlineData(null)]
        public void CheckUserNameTestNull(string username)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var userNameResult = authenticationController.CheckUserName(username);
            Assert.Equal(UserNameResultType.UserNameNullError, userNameResult);
        }

        [Theory]
        [InlineData("5")]
        [InlineData("A5555555*")]
        [InlineData("A5555555A")]
        public void CheckIdTestIncorrect(string id)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var idResult = authenticationController.CheckId(id);
            Assert.Equal(IdResultType.IdIncorrect, idResult);
        }

        [Theory]
        [InlineData(null)]
        public void CheckIdTestNull(string id)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var idResult = authenticationController.CheckId(id);
            Assert.Equal(IdResultType.IdNullError, idResult);
        }

        [Theory]
        [InlineData("S1111110S")]
        public void CheckIdTestDuplicate(string id)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var idResult = authenticationController.CheckId(id);
            Assert.Equal(IdResultType.DuplicateId, idResult);
        }


        [Theory]
        [InlineData(null)]
        public void CheckPhoneNumberTestNull(string phoneNumber)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var phoneNumberResult = authenticationController.CheckPhoneNumber(phoneNumber);
            Assert.Equal(PhoneNumberResultType.PhoneNumberNullError, phoneNumberResult);
        }

        [Theory]
        [InlineData("91234")]
        [InlineData("9123411A")]
        public void CheckPhoneNumberTestIncorrect(string phoneNumber)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var phoneNumberResult = authenticationController.CheckPhoneNumber(phoneNumber);
            Assert.Equal(PhoneNumberResultType.PhoneNumberIncorrect, phoneNumberResult);
        }

        [Theory]
        [InlineData("91234111")]
        public void CheckPhoneNumberTestNone(string phoneNumber)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var phoneNumberResult = authenticationController.CheckPhoneNumber(phoneNumber);
            Assert.Equal(PhoneNumberResultType.None, phoneNumberResult);
        }

        [Theory]
        [InlineData("99999990")]
        public void CheckPhoneNumberTestDuplicate(string phoneNumber)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!")
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var phoneNumberResult = authenticationController.CheckPhoneNumber(phoneNumber);
            Assert.Equal(PhoneNumberResultType.DuplicatePhoneNumber, phoneNumberResult);
        }

        [Theory]
        [InlineData("Happy123!")]
        public void CheckPasswordTestNone(string passWord)
        {
            var authenticationController = new AuthenticationController();

            var passwordResult = authenticationController.CheckPassword(passWord);
            Assert.Equal(PasswordResultType.None, passwordResult);
        }

        [Theory]
        [InlineData("")]
        public void CheckPasswordTestNoUpperLowerDigitSpecialCase(string passWord)
        {
            var authenticationController = new AuthenticationController();

            var passwordResult = authenticationController.CheckPassword(passWord);
            Assert.Equal(PasswordResultType.None
                | PasswordResultType.IncorrectPasswordLength
                | PasswordResultType.PasswordNoDigits
                | PasswordResultType.PasswordNoUpperCaseLetter
                | PasswordResultType.PasswordNoLowerCaseLetter
                | PasswordResultType.PasswordNoSpecialCharacter
                , passwordResult);
        }

        [Theory]
        [InlineData(null)]
        public void CheckPasswordTestNull(string passWord)
        {
            var authenticationController = new AuthenticationController();

            var passwordResult = authenticationController.CheckPassword(passWord);
            Assert.Equal(PasswordResultType.PasswordNullError, passwordResult);
        }

        [Theory]
        [InlineData("Happpy123!")]
        public void CheckPasswordTestRepeatedChar(string passWord)
        {
            var authenticationController = new AuthenticationController();

            var passwordResult = authenticationController.CheckPassword(passWord);
            Assert.Equal(PasswordResultType.None | PasswordResultType.PasswordThreeRepeatedCharacters, passwordResult);
        }

        [Theory]
        [InlineData("Happy123", "S1111110S", "99999990", "Happy123!")]
        public void SignUpTest(string userName, string idNumber, string phoneNumber, string passWord)
        {
            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);
            _mockCasinoContext.Setup(t => t.SaveChanges()).Returns(1);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var signUpResult = authenticationController.SignUp(userName, idNumber, phoneNumber, passWord);

            mockUserDbSet.Verify(t => t.Add(It.IsAny<Models.User>()), Times.Once());
            _mockCasinoContext.Verify(t => t.SaveChanges(), Times.Once());
            Assert.True(signUpResult);
        }

        [Theory]
        [InlineData("Owner", "Owner")]
        public void LoginTestOwner(string userName, string passWord)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!"),
                new Models.User("Owner", "S1111111S", "99999999", "Owner", true)
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var idResult = authenticationController.Login(userName, passWord);
            Assert.Equal((true, true), idResult);
            ;
        }

        [Theory]
        [InlineData("Happy123", "Happy123!")]
        public void LoginTestUser(string userName, string passWord)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!"),
                new Models.User("Owner", "S1111111S", "99999999", "Owner", true)
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var idResult = authenticationController.Login(userName, passWord);
            Assert.Equal((true, false), idResult);
            ;
        }

        [Theory]
        [InlineData("Ha123", "Happ3!")]
        public void LoginTestFail(string userName, string passWord)
        {
            var userList = new List<Models.User>
            {
                new Models.User("Happy123", "S1111110S", "99999990", "Happy123!"),
                new Models.User("Owner", "S1111111S", "99999999", "Owner", true)
            }.AsQueryable();

            var mockUserDbSet = new Mock<DbSet<Models.User>>();
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Provider).Returns(userList.Provider);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.Expression).Returns(userList.Expression);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.ElementType).Returns(userList.ElementType);
            mockUserDbSet.As<IQueryable<Models.User>>().Setup(t => t.GetEnumerator()).Returns(userList.GetEnumerator());

            _mockCasinoContext.Setup(t => t.Users).Returns(mockUserDbSet.Object);

            var authenticationController = new AuthenticationController(_mockCasinoContext.Object);

            var idResult = authenticationController.Login(userName, passWord);
            Assert.Equal((false, false), idResult);
            ;
        }

        [Fact]
        public void LogoutTest()
        {
            var authenticationController = new AuthenticationController();

            Assert.False(authenticationController.Logout());
        }
    }
}
