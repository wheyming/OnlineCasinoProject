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
    public class CasinoViewModelSignUpLoginLogoutTest
    {
        public CasinoViewModelSignUpLoginLogoutTest()
        {
        }

        [Theory]
        [InlineData("Happy", "Happy", "Happy", "Happy")]
        public void SignUpTestFailed(string userName, string idNumber, string phoneNumber, string passWord)
        {
            var expectedResult = true;
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

            var result = underTest.SignUp(userName, idNumber, phoneNumber, passWord);

            Assert.Equal("Error has occured.", result);
        }

        [Theory]
        [InlineData("Happy", "Happy", "Happy", "Happy")]
        public void SignUpTestSuccess(string userName, string idNumber, string phoneNumber, string passWord)
        {
            var expectedResult = true;
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

            var result = underTest.SignUp(userName, idNumber, phoneNumber, passWord);

            Assert.Equal("New account has been registered.", result);
        }

        [Theory]
        [InlineData("Happy", "Happy")]
        public void LoginTestSuccessOwner(string userName, string passWord)
        {
            var expectedResult = (true, true);
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

            var result = underTest.CheckLogin(userName, passWord);

            Assert.Equal(("\nWelcome Owner." +
                        "\nWould you like to" +
                        "\n1.) Set prize module?" +
                        "\n2.) View financial report for a certain day?" +
                        "\n3.) View financial report for a certain month?" +
                        "\n4.) View financial report for a certain year?" +
                        "\n5.) Log out.", true), result);
        }

        [Theory]
        [InlineData("Happy", "Happy")]
        public void LoginTestSuccessUser(string userName, string passWord)
        {
            var expectedResult = (true, false);
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

            var result = underTest.CheckLogin(userName, passWord);

            Assert.Equal(($"Welcome Happy." +
                        "\nWould you like to" +
                        "\n1.) Play Slots?" +
                        "\n2.) Logout?", false), result);
        }

        [Fact]
        public void LogoutTest()
        {
            var expectedResult = false;
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

            var result = underTest.LogOut();

            Assert.False(result);
        }
    }
}
