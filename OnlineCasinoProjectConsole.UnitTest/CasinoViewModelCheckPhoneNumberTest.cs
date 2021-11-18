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
    public class CasinoViewModelCheckPhoneNumberTest
    {
        public CasinoViewModelCheckPhoneNumberTest()
        {
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckPhoneNumberTestNone(string phoneNumber)
        {
            var expectedResult = PhoneNumberResultType.None;
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

            var result = underTest.CheckPhoneNumber(phoneNumber);

            Assert.Equal(string.Empty, result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckPhoneNumberTestDuplicate(string phoneNumber)
        {
            var expectedResult = PhoneNumberResultType.DuplicatePhoneNumber;
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

            var result = underTest.CheckPhoneNumber(phoneNumber);

            Assert.Equal("Duplicate Phone Number.", result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckPhoneNumberTestUnhandled(string phoneNumber)
        {
            var expectedResult = PhoneNumberResultType.UnhandledPhoneNumberError;
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

            var result = underTest.CheckPhoneNumber(phoneNumber);

            Assert.Equal("Unexpected Error.", result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckPhoneNumberTestNull(string phoneNumber)
        {
            var expectedResult = PhoneNumberResultType.PhoneNumberNullError;
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

            var result = underTest.CheckPhoneNumber(phoneNumber);

            Assert.Equal("Input cannot be Null.", result);
        }

        [Theory]
        [InlineData("Happy")]
        public void CheckPhoneNumberTestIncorrect(string phoneNumber)
        {
            var expectedResult = PhoneNumberResultType.PhoneNumberIncorrect;
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

            var result = underTest.CheckPhoneNumber(phoneNumber);

            Assert.Equal("Invalid Phone Number", result);
        }
    }
}
