using Casino.Common;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using OnlineCasinoProjectConsole.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnlineCasinoProjectConsole.UnitTest
{
    public class CasinoViewModelCheckPasswordTest
    {
        private string _uriName;
        public CasinoViewModelCheckPasswordTest()
        {
#if DEBUG
            _uriName = "https://localhost:44353/";
#else
            _uriName = "http://mycasino.me";
#endif
        }

        [Fact]
        public void CheckPasswordTestNone()
        {
            var expectedResult = PasswordResultType.None;
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPassword(_uriName);
            IList<string> outputList = new List<string>();

            Assert.Equal(outputList, result);
        }

        [Fact]
        public void CheckPasswordTest()
        {
            var expectedResult = PasswordResultType.PasswordNoDigits |
                PasswordResultType.PasswordNoUpperCaseLetter |
                PasswordResultType.PasswordNoLowerCaseLetter |
                PasswordResultType.PasswordNoSpecialCharacter |
                PasswordResultType.PasswordThreeRepeatedCharacters |
                PasswordResultType.IncorrectPasswordLength;
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPassword(_uriName);
            IList<string> outputList = new List<string>()
            {
                "Please choose a password with at least one uppercase letter.",
                "Please choose a password with at least one lowercase letter.",
                "Please choose a password with at least one special character.",
                "Please choose a password with at least one digit.",
                "Please choose a password with at most two continuous repeated characters.",
                "Please choose a password between 6 to 24 characters."
            };

            Assert.Equal(outputList, result);
        }

        [Fact]
        public void CheckPhoneNumberTestUnhandled()
        {
            var expectedResult = PasswordResultType.UnhandledPasswordError;
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPassword(_uriName);
            IList<string> outputList = new List<string>()
            {
                "Please contact administrator."
            };

            Assert.Equal(outputList, result);
        }

        [Fact]
        public void CheckPasswordTestNull()
        {
            var expectedResult = PasswordResultType.PasswordNullError;
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPassword(_uriName);
            IList<string> outputList = new List<string>()
            {
                "Please do not leave password blank."
            };

            Assert.Equal(outputList, result);
        }
    }
}
