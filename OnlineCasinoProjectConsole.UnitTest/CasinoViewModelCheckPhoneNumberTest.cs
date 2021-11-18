using Casino.Common;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
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
        private string _uriName;
        public CasinoViewModelCheckPhoneNumberTest()
        {
#if DEBUG
            _uriName = "https://localhost:44353/";
#else
            _uriName = "http://mycasino.me";
#endif
        }

        [Fact]
        public void CheckPhoneNumberTestNone()
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPhoneNumber(_uriName);

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void CheckPhoneNumberTestDuplicate()
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPhoneNumber(_uriName);

            Assert.Equal("Duplicate Phone Number.", result);
        }

        [Fact]
        public void CheckPhoneNumberTestUnhandled()
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPhoneNumber(_uriName);

            Assert.Equal("Unexpected Error.", result);
        }

        [Fact]
        public void CheckPhoneNumberTestNull()
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPhoneNumber(_uriName);

            Assert.Equal("Input cannot be Null.", result);
        }

        [Fact]
        public void CheckPhoneNumberTestIncorrect()
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
            var underTest = new CasinoViewModel(new HttpClient(mockMessageHandler.Object));

            var result = underTest.CheckPhoneNumber(_uriName);

            Assert.Equal("Invalid Phone Number", result);
        }
    }
}
