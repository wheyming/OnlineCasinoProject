using Casino.Common;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using OnlineCasinoProjectConsole.Utility;
using OnlineCasinoProjectConsole.ViewModel;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineCasinoProjectConsole.UnitTest
{
    public class CasinoViewModelCheckUserNameTest
    {
        public CasinoViewModelCheckUserNameTest()
        { }

        [Theory]
        [InlineData("Happy")]
        public void CheckUsernameTestNone(string userName)
        {
            var expectedResult = UserNameResultType.None;
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(), 
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.CheckUserName(userName);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckUsernameTestDuplicate(string userName)
        {
            var expectedResult = UserNameResultType.DuplicateUser;
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.CheckUserName(userName);

            Assert.Equal("Duplicate Username.", result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckUsernameTestDuplicateUnhandled(string userName)
        {
            var expectedResult = UserNameResultType.UnhandledUserError;
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.CheckUserName(userName);

            Assert.Equal("Unexpected Error.", result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckUsernameTestContainsSpace(string userName)
        {
            var expectedResult = UserNameResultType.UserNameContainsSpace;
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.CheckUserName(userName);

            Assert.Equal("Please create a Username without Space.", result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckUsernameTestNull(string userName)
        {
            var expectedResult = UserNameResultType.UserNameNullError;
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.CheckUserName(userName);

            Assert.Equal("Input cannot be Null.", result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckUsernameTestLengthIncorrect(string userName)
        {
            var expectedResult = UserNameResultType.UserNameLengthIncorrect;
            var json = JsonConvert.SerializeObject(expectedResult);

            var mockMessageHandler = new Mock<HttpMessageHandler>();

            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object), new DateConverter());

            var result = underTest.CheckUserName(userName);

            Assert.Equal("Please create a username between 6 to 24 characters.", result);
        }

    }
}
